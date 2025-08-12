using UnityEngine;

public class GroundChecker2D : MonoBehaviour
{
    [Header("체크 기준점(발 위치)")]
    public Transform groundCheck;

    [Header("BoxCast 설정")]
    public float centerDownOffset = 0.05f;
    public Vector2 boxSize = new Vector2(0.2f, 0.05f);
    public float castDistance = 0.01f;

    [Header("지면 레이어/노멀 임계값")]
    public LayerMask groundLayer;
    [Range(0f, 1f)] public float normalYThreshold = 0.7f;

    [Header("디버그")]
    public bool drawGizmos = true;

    public bool IsGrounded { get; private set; }
    public Vector2 LastNormal { get; private set; } // 디버그용

    public void Refresh()
    {
        IsGrounded = CheckGrounded();
    }

    public bool CheckNow()
    {
        Refresh();
        return IsGrounded;
    }

    private bool CheckGrounded()
    {
        if (groundCheck == null) groundCheck = transform;

        Vector2 boxCenter = (Vector2)groundCheck.position + Vector2.down * centerDownOffset;
        Vector2 size = boxSize;

        RaycastHit2D hit = Physics2D.BoxCast(boxCenter, size, 0f, Vector2.down, castDistance, groundLayer);

        if (hit.collider != null)
        {
            LastNormal = hit.normal;
            return hit.normal.y > normalYThreshold;
        }

        LastNormal = Vector2.zero;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;
        if (groundCheck == null) groundCheck = transform;

        Vector2 boxCenter = (Vector2)groundCheck.position + Vector2.down * centerDownOffset;
        Gizmos.color = IsGrounded ? Color.green : Color.red;

        Gizmos.DrawWireCube(boxCenter, boxSize);

        Vector3 from = boxCenter;
        Vector3 to = boxCenter + Vector2.down * castDistance;
        Gizmos.DrawLine(from, to);
    }
}
