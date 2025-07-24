using TMPro;
using UnityEngine;

public class HeldItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;

    public void SetItemName(string itemName)
    {
        if (string.IsNullOrWhiteSpace(itemName))
        {
            nameText.text = "";
        }
        else
        {
            nameText.text = $"{itemName}";
        }
    }

    public void ClearItemName()
    {
        nameText.text = "";
    }
}
