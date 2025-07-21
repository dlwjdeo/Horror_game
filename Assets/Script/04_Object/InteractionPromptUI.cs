using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    public GameObject message;
    public TextMeshPro promptText;

    public void Show(string message)
    {
        this.message.SetActive(true);
        promptText.text = message;
    }

    public void Hide()
    {
        this.message.SetActive(false);
    }
}
