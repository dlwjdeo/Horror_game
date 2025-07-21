using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;

    void Awake()
    {
        HideMessage();
    }

    public void ShowMessage(string message)
    {
        panel.SetActive(true);
        messageText.text = message;
        StartCoroutine(HideMessage(3));
    }

    public void HideMessage()
    {
        panel.SetActive(false);
    }

    IEnumerator HideMessage(int sec)
    {
        yield return new WaitForSeconds(sec);
        HideMessage();
    }
}
