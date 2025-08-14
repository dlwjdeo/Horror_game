using System;
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable, IPromptSource
{
    public ItemType itemType;

    public string promptText => throw new System.NotImplementedException();

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