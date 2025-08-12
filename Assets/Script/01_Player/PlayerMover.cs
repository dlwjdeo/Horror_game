using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerStateMachine))]
public class PlayerMover : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("점프 설정")]
    public int maxJumps = 1;
    private int remainingJumps;

    [Header("계단 이동")]
    public float climbSpeed = 2.5f;

    [Header("참조")]
    public GroundChecker2D groundChecker;

    private Rigidbody2D rb;
    private PlayerStateMachine playerStateMachine;
    private float defaultGravity;

    private bool prevGrounded;
    private PlayerState prevState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStateMachine = GetComponent<PlayerStateMachine>();
        defaultGravity = rb.gravityScale;
        remainingJumps = maxJumps;
    }

    private void Update()
    {
        if (groundChecker != null) groundChecker.Refresh();
        bool grounded = groundChecker != null && groundChecker.IsGrounded;

        if (grounded && !prevGrounded && !playerStateMachine.Is(PlayerState.Stair))
        {
            remainingJumps = maxJumps;
        }

        if (grounded && prevState == PlayerState.Stair && !playerStateMachine.Is(PlayerState.Stair))
        {
            remainingJumps = maxJumps;
        }

        prevGrounded = grounded;
        prevState = playerStateMachine.Current;
    }

    private void FixedUpdate()
    {
        var move = PlayerInputManager.Instance.GetMove();

        switch (playerStateMachine.Current)
        {
            case PlayerState.Stair:
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(0f, move.y * climbSpeed);
                break;

            default:
                rb.gravityScale = defaultGravity; 
                rb.velocity = new Vector2(move.x * moveSpeed, rb.velocity.y);
                break;
        }
    }

    private void OnEnable()
    {
        PlayerInputManager.Instance.Jump += TryJump;
    }
    private void OnDisable()
    {
        PlayerInputManager.Instance.Jump -= TryJump;
    }
    private void TryJump()
    {
        if (playerStateMachine.Is(PlayerState.Stair)) return;
        if (remainingJumps <= 0) return;

        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        remainingJumps--;
    }
}
