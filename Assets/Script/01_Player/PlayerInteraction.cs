using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // 지금 닿아있는 상호작용 대상(가장 최근에 들어온 것 1개)
    private IInteractable _current;

    //접촉 시작
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var it))
            _current = it;
    }

    // 접촉 종료
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_current != null && other.TryGetComponent<IInteractable>(out var it) && it == _current)
            _current = null;
    }

    private void OnDisable() => _current = null;

    // Player에서 interactDown일 때 호출
    public void TryInteract()
    {
        _current?.Interact(gameObject);     // 닿아있는 대상이 있으면 실행
    }
}
