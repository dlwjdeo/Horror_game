using UnityEngine;

public class LampCharger : MonoBehaviour, IInteractable, IPromptSource
{
    public string promptText => throw new System.NotImplementedException();

    public void Interact(GameObject interactor)
    {
        throw new System.NotImplementedException();
    }
}
