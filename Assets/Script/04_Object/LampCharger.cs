using System;
using UnityEngine;

public class LampCharger : MonoBehaviour, IInteractable, IPromptSource
{
    public string promptText
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public InteractionPromptUI interactionPromptUI => throw new NotImplementedException();

    public event Action<string> Interacted;


    public void HideMessage()
    {
        throw new NotImplementedException();
    }

    public void Interact(GameObject interactor)
    {
        throw new System.NotImplementedException();
    }

    public void ShowMessage(string message)
    {
        throw new System.NotImplementedException();
    }
}
