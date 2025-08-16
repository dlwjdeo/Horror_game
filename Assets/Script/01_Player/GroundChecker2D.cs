using UnityEngine;

[DefaultExecutionOrder(-50)] // Player/PlayerMover(기본 0)보다 먼저 실행되게 함
public class GroundChecker2D : MonoBehaviour
{
    [Header("체크 기준점(발 위치)")]
    public Transform groundCheck;                 // 없으면 현재 transform 사용

    [Header("BoxCast 설정")]
    public Vector2 boxSize = new Vector2(0.2f, 0.05f);
    public float castDistance = 0.02f;            // 아래 방향으로 얼마만큼 쏠지
    public LayerMask groundLayer;                 // 지면으로 취급할 레이어

    [Header("디버그")]
    public bool drawGizmos = true;

    // 결과값 (읽기 전용)
    public bool IsGrounded { get; private set; }
    public RaycastHit2D LastHit { get; private set; }
    public Collider2D GroundCollider { get; private set; }

    public void Refresh()
    {
        Vector2 origin = groundCheck ? (Vector2)groundCheck.position : (Vector2)transform.position;

        var hit = Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, castDistance, groundLayer);

        if (hit.collider != null && hit.normal.y >= 0.9f)
        {
            IsGrounded = true;
            LastHit = hit;
            GroundCollider = hit.collider;
        }
        else
        {
            IsGrounded = false;
            LastHit = default;
            GroundCollider = null;
        }
    }

    private void FixedUpdate()
    {
        Refresh();
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        Vector2 origin = groundCheck ? (Vector2)groundCheck.position : (Vector2)transform.position;

        Gizmos.color = IsGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(origin + Vector2.down * castDistance, boxSize);

        if (IsGrounded)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(LastHit.point, 0.03f); // 접지 지점만 표시(법선/각도 제거)
        }
    }

    private void OnValidate()
    {
        boxSize = new Vector2(Mathf.Max(0.001f, boxSize.x), Mathf.Max(0.001f, boxSize.y));
        castDistance = Mathf.Max(0.0001f, castDistance);
    }
}
