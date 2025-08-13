using UnityEngine;

public class Chest : MonoBehaviour, IPromptSource, IInteractable
{
    public string promptText => throw new System.NotImplementedException();

    public void Interact(GameObject interactor)
    {
        throw new System.NotImplementedException();
    }
}
