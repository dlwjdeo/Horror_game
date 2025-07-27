using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerStateMachine))]
public class Player : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Ground 체크")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("점프 설정")]
    public int maxJumps = 1;
    private int remainingJumps;

    private Rigidbody2D rb;
    private PlayerStateMachine stateMachine;
    private PlayerInventory inventory;

    private float moveInputX;
    private float moveInputY;
    public bool isGrounded = false;
    public bool isStair = false;
    public bool isOnStair = false;

    private float stairEntryBlockTime = 1f;
    private float stairEntryBlockTimer = 0f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = GetComponent<PlayerStateMachine>();
        inventory = GetComponent<PlayerInventory>();
        remainingJumps = maxJumps;
    }

    private void Update()
    {
        GroundCheck();
        if(GameManager.Instance.GetInputBlockState()) return;
        InputCheck();
        ItemDrop();
        UpdateStairBlockTimer();
        Jump();
        UpdateState();
    }

    private void FixedUpdate()
    {
        if (isOnStair)
        {
            rb.velocity = new Vector2(0f, moveInputY * moveSpeed);
            return;
        }
        else
        {

            rb.gravityScale = 5f;
            rb.velocity = new Vector2(moveInputX * moveSpeed, rb.velocity.y);
        }
    }

    private void ItemDrop()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.IsHoldingItem())
        {
            float facingDir = transform.localScale.x > 0 ? 1f : -1f;
            Vector3 dropPosition = transform.position + new Vector3(facingDir, 0f, 0f);
            inventory.DropItem(dropPosition);
        }
    }
    private void UpdateStairBlockTimer()
    {
        if (stairEntryBlockTimer > 0f)
            stairEntryBlockTimer -= Time.deltaTime;
    }

    private void InputCheck()
    {
        moveInputX = Input.GetAxisRaw("Horizontal");

        if (isStair)
        {
            moveInputY = Input.GetAxisRaw("Vertical");

            // 계단 진입 조건: 아래 발판에 붙어있고 위/아래 키 입력 있을 때 + 타이머 조건
            if (!isOnStair && stairEntryBlockTimer <= 0f && Mathf.Abs(moveInputY) > 0.1f)
            {
                isOnStair = true;
                rb.gravityScale = 0f;
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Stair);
            }
        }
        else
        {
            moveInputY = 0f;
        }
    }

    private void GroundCheck()
    {
        isGrounded = IsGroundedAccurate();

        if (isGrounded && !isOnStair)
            remainingJumps = maxJumps;

        // 계단 상태일 때는 탈출 조건을 따로 분리해서 검사
        if (isOnStair && isStair == false && isGrounded)
        {
            isOnStair = false;
            rb.gravityScale = 5f;
            if(transform.position.y > 10f)
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor2);
            }
            else
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor1);
            }
        }
    }

    private bool IsGroundedAccurate()
    {
        // 중심을 위로 0.05f 올림
        Vector2 boxCenter = (Vector2)groundCheck.position + Vector2.down * 0.05f;
        Vector2 boxSize = new Vector2(0.2f, 0.05f);

        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, boxSize, 0f, Vector2.down, 0.01f, groundLayer);

        if (hit.collider != null)
        {
            return hit.normal.y > 0.7f;
        }
        return false;
    }

    private void Jump()
    {
        if (isOnStair) return; // 계단 중에는 점프 불가

        if (Input.GetKeyDown(KeyCode.Space) && remainingJumps > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            remainingJumps--;
            stateMachine.SetState(PlayerState.Jumping);
        }
    }

    private void UpdateState()
    {
        if (stateMachine.Is(PlayerState.Hidden) ||
            stateMachine.Is(PlayerState.Interacting) ||
            stateMachine.Is(PlayerState.GameOver))
            return;

        if (!isGrounded)
        {
            if (rb.velocity.y > 0.1f)
                stateMachine.SetState(PlayerState.Jumping);
            else if (rb.velocity.y < -0.1f)
                stateMachine.SetState(PlayerState.Falling);
        }
        else if (Mathf.Abs(moveInputX) > 0.1f)
        {
            stateMachine.SetState(PlayerState.Moving);
        }
        else
        {
            stateMachine.SetState(PlayerState.Idle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Stair))
        {
            isStair = true;
        }
        if (collision.CompareTag(TagName.Ground) && isGrounded)
        {
            isOnStair = false;
            stairEntryBlockTimer = stairEntryBlockTime;
            if (transform.position.y > 10f)
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor2);
            }
            else
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor1);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagName.Stair))
        {
            isStair = false;
            isOnStair = false;
            rb.gravityScale = 5f;
            rb.velocity = Vector2.zero; 
            stairEntryBlockTimer = stairEntryBlockTime;
            if (transform.position.y > 10f)
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor2);
            }
            else
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(TagName.Ground) && isGrounded)
        {
            isOnStair = false;
            stairEntryBlockTimer = stairEntryBlockTime;
            if (transform.position.y > 10f)
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor2);
            }
            else
            {
                GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor1);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Vector2 boxCenter = groundCheck.position + Vector3.down * 0.05f;
            Vector2 boxSize = new Vector2(0.2f, 0.05f);
            Gizmos.DrawWireCube(boxCenter, boxSize);
        }
    }
}