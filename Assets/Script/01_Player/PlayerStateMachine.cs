using UnityEngine;
public class PlayerStateMachine : MonoBehaviour
{
    [field: SerializeField]
    public PlayerState Current { get; private set; } = PlayerState.Idle;

    public bool Is(PlayerState s) => Current == s;
    public void SetState(PlayerState s)
    {
        if (Current == s) return;
        Current = s;
    }
}