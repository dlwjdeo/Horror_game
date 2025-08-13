using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // 지금 닿아있는 상호작용 대상(가장 최근에 들어온 것 1개)
    private IInteractable _currentInteractable; // 상호작용 내용 실행
    private IPromptSource _currentPromptSource; // prompt내용 받은 후 출력

    //접촉 시작
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactable)) 
        {
            _currentInteractable = interactable;
        }
        if (other.TryGetComponent<IPromptSource>(out var promptSource))
        {
            _currentPromptSource = promptSource;
        }
    }

    // 접촉 종료
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_currentInteractable != null && other.TryGetComponent<IInteractable>(out var interactable) && interactable == _currentInteractable)
        {
            _currentInteractable = null;
        }
        if(_currentPromptSource != null && other.TryGetComponent<IPromptSource>(out var promptSource) && promptSource == _currentPromptSource)
        {
            _currentPromptSource = null;    
        }
    }

    private void OnDisable()
    {
        _currentInteractable = null;
    }
    // Player에서 interactDown일 때 호출
    public void TryInteract()
    {
        _currentInteractable?.Interact(gameObject);     // 닿아있는 대상이 있으면 실행
    }
}
