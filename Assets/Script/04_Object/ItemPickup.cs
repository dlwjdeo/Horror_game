using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable, IPromptSource
{
    public ItemType itemType;

    [SerializeField]
    private string promptMessage = "E키를 눌러 아이템 줍기";
    [SerializeField]
    private string interactMessage; //내부용 프로퍼티

    public event Action<string> Interacted;

    public string promptText => promptMessage;  //외부용 프로퍼티

    public InteractionPromptUI interactionPromptUI { get; private set; }

    private void Awake()
    {
        interactionPromptUI = GetComponentInChildren<InteractionPromptUI>();
        interactMessage = $"{itemType}을(를) 획득했다";
    }


    public void Interact(GameObject interactor)
    {
        var inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null && !inventory.IsHoldingItem())
        {
            inventory.HoldItem(this);
        }
        else
        {
            UIManager.Instance.interactionUI.ShowMessage("이미 아이템을 들고 있습니다.");
        }
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


    /*private void Awake()
    {
        promptMessage = "E키를 눌러 아이템 줍기";
        interactMessage = $"{itemType}을(를) 획득했다";
    }

    protected override void Interact()
    {
        base.Interact();

        var inventory = player.GetComponent<PlayerInventory>();
        if (inventory != null && !inventory.IsHoldingItem())
        {
            promptUI.Hide();
            inventory.HoldItem(this);
        }
        else
        {
            UIManager.Instance.interactionUI.ShowMessage("이미 아이템을 들고 있습니다.");
        }
    }

    public void ResetInteractable()
    {
        isPlayerInRange = false;
    }*/
}