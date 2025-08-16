// PlayerMover.cs
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private GroundChecker2D groundChecker2D;

    [Header("Move")]
    [SerializeField] private float moveSpeed = 6f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 7f;

    [Header("Gravity")]
    [SerializeField] private float baseGravityScale = 3.5f;

    private Rigidbody2D _rigidbody2D;
    private bool jumpRequested;
    public void RequestJump() => jumpRequested = true;

    public bool IsGrounded => groundChecker2D && groundChecker2D.IsGrounded;
    public bool IsOnStair => isOnStair;

    private bool isOnStair;
    private bool isStair;
    private float exitStairTimer;
    private float enterStairTimer;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        groundChecker2D = GetComponent<GroundChecker2D>();

        _rigidbody2D.gravityScale = baseGravityScale;
        _rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        if(exitStairTimer >= 0)
        {
            exitStairTimer -= Time.deltaTime;
        }
        if(enterStairTimer >= 0)
        {
            enterStairTimer -= Time.deltaTime;
        }
        UpdateGroundCheck();
    }

    public void TickMove(float x, float y)
    {
        if (!isOnStair) 
            ApplyHorizontal(x);
        else
            ApplyVerticalOnStair(y);
    }

    public void TryJump()
    {
        if (!jumpRequested) return;
        jumpRequested = false;

        if(!IsGrounded) return;
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ApplyHorizontal(float x)
    {
        _rigidbody2D.velocity = new Vector2(x * moveSpeed, _rigidbody2D.velocity.y);
    }
    private void ApplyVerticalOnStair(float y)
    {
        _rigidbody2D.velocity = new Vector2(0, y * moveSpeed);
    }

    public void SetGravityScale(float gravityScale)
    {
        _rigidbody2D.gravityScale = gravityScale;
    }

    public void EnterStair()
    {
        if(!isStair && enterStairTimer <= 0) return;
        Debug.Log("입장");
        isOnStair = true;
        SetGravityScale(0f);
        exitStairTimer = 0.5f;
        gameObject.layer = LayerMask.NameToLayer("PlayerStair");
    }
    public void ExitStair()
    {
        Debug.Log("탈출");
        enterStairTimer = 2f;
        isOnStair = false;
        SetGravityScale(baseGravityScale);
        gameObject.layer = LayerMask.NameToLayer("Player");
    }
    public void SetStair(bool stair)
    { 
        isStair = stair;
    }

    public bool GetStair()
    {
        return isStair;
    }

    private void UpdateGroundCheck()
    {
        if(exitStairTimer <= 0 && IsGrounded && isOnStair)
            ExitStair();
    }
}
