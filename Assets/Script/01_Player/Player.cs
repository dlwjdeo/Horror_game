using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine))]
public class Player : MonoBehaviour
{
    public GroundChecker2D groundChecker; // 네가 쓰던 체크러 사용
    public float stairExitGrace = 0.1f;

    private PlayerStateMachine playerStateMachine;
    private float stairLostAt = float.NegativeInfinity;

    

    private void Awake()
    {
        playerStateMachine = GetComponent<PlayerStateMachine>();
    }

    private void Update()
    {
        var input = PlayerInputManager.Instance;
        var move = input.GetMove();
        bool inStair = input.isInStairTrigger;
        bool grounded = groundChecker != null && groundChecker.IsGrounded;

        if (playerStateMachine.Is(PlayerState.Stair))
        {
            if (groundChecker.IsGrounded)
            {
                playerStateMachine.SetState(PlayerState.Idle);
                return;
            }

            if (inStair)
            {
                stairLostAt = float.NegativeInfinity;
            }
            else
            {
                if (float.IsNegativeInfinity(stairLostAt))
                    stairLostAt = Time.fixedTime;

                if (Time.fixedTime - stairLostAt >= stairExitGrace)
                {
                    playerStateMachine.SetState(PlayerState.Idle);
                    return;
                }
            }

            return;
        }

        if (inStair && Mathf.Abs(move.y) > 0.1f)
        {
            playerStateMachine.SetState(PlayerState.Stair);
            return;
        }

        if (!grounded)
        {
            if (GetComponent<Rigidbody2D>().velocity.y > 0.1f) playerStateMachine.SetState(PlayerState.Jumping);
            else if (GetComponent<Rigidbody2D>().velocity.y < -0.1f) playerStateMachine.SetState(PlayerState.Falling);
        }
        else
        {
            if (Mathf.Abs(move.x) > 0.1f) playerStateMachine.SetState(PlayerState.Moving);
            else playerStateMachine.SetState(PlayerState.Idle);
        }
    }
}
