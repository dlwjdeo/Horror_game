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
    [SerializeField] private float maxFallSpeed = -16f;

    private Rigidbody2D _rigidbody2D;
    private bool jumpRequested;
    public void RequestJump() => jumpRequested = true;

    public bool IsGrounded => groundChecker2D && groundChecker2D.IsGrounded;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        groundChecker2D = GetComponent<GroundChecker2D>();

        _rigidbody2D.gravityScale = baseGravityScale;
        _rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
        _rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void TickMove(float x, float y)
    {
        ApplyHorizontal(x);
        ApplyVertical();
    }

    public void TryJump()
    {
        if (!jumpRequested) return;
        jumpRequested = false;
        Debug.Log("리퀴스트 성공");

        if(!IsGrounded) return;
        Debug.Log("점프 진입 성공");
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ApplyHorizontal(float x)
    {
        _rigidbody2D.velocity = new Vector2(x * moveSpeed, _rigidbody2D.velocity.y);
    }
    private void ApplyVertical()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Max(_rigidbody2D.velocity.y, maxFallSpeed));
    }
}
