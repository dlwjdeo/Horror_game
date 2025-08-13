using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private ItemPickup heldItem;

    public bool IsHoldingItem() => heldItem != null;

    public void HoldItem(ItemPickup item)
    {
        heldItem = item;
        heldItem.gameObject.SetActive(false);
        heldItem.transform.SetParent(transform);

        UIManager.Instance.heldItemUI.SetItemName(heldItem.itemType.ToString());
    }

    public void DropItem(Vector3 dropPosition)
    {
        if (heldItem == null) return;

        heldItem.transform.SetParent(null);
        heldItem.transform.position = dropPosition;
        heldItem.gameObject.SetActive(true);
        //heldItem.ResetInteractable(); 
        heldItem = null;

        UIManager.Instance.heldItemUI.ClearItemName();
    }

    public ItemType? GetHeldItemType()
    {
        return heldItem != null ? heldItem.itemType : (ItemType?)null;
    }
}