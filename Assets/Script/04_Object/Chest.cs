using System;
using UnityEngine;

public class Chest : MonoBehaviour, IPromptSource, IInteractable
{
    [SerializeField]
    private string promptMessage = "E키를 눌러 열기";
    [SerializeField]
    private string interactMessage = "안에는 아무것도 없다"; //내부용 프로퍼티

    public event Action<string> Interacted;

    public string promptText => promptMessage;  //외부용 프로퍼티

    public InteractionPromptUI interactionPromptUI { get; private set; }

    private void Awake()
    {
        interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();   
    }

    public void Interact(GameObject interactor)
    {
        Interacted?.Invoke(interactMessage);
    }

    public void ShowMessage(string message)
    {
        interactionPromptUI.Show(message);
    }

    public void HideMessage()
    {
        interactionPromptUI.Hide();
    }
}
