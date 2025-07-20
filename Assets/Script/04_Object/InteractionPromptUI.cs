using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionPromptUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshPro promptText;

    public void Show(string message)
    {
        panel.SetActive(true);
        promptText.text = message;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
