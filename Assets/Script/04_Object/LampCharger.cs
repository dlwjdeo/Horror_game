using System;
using UnityEngine;

public class LampCharger : MonoBehaviour, IInteractable, IPromptSource
{
    [SerializeField]
    private string promptMessage = "E키를 눌러 충전하기";
    [SerializeField]
    private string interactMessage = "램프가 따뜻해졌다"; //내부용 프로퍼티
    [SerializeField]
    private float chargeAmount = 30f;

    public event Action<string> Interacted;

    public string promptText => promptMessage;  //외부용 프로퍼티

    public InteractionPromptUI interactionPromptUI { get; private set; }

    private void Awake()
    {
        interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();
    }


    public void Interact(GameObject interactor)
    {
        TryChargeLamp();
        Interacted?.Invoke(interactMessage);
        Destroy(this);
    }

    public void ShowMessage(string message)
    {
        interactionPromptUI.Show(message);
    }

    public void HideMessage()
    {
        interactionPromptUI.Hide();
    }

    private void TryChargeLamp()
    {
        UIManager.Instance.ChargeLamp(chargeAmount);
    }
}
