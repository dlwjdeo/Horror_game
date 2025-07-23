using UnityEngine;
public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState { get; private set; } = PlayerState.Idle;

    public void SetState(PlayerState newState)
    {
        if (CurrentState != newState)
        {
            //Debug.Log($"[PlayerState] {CurrentState} ¡æ {newState}");
            CurrentState = newState;
        }
    }

    public bool Is(PlayerState state) => CurrentState == state;
}