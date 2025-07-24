using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string promptMessage = "E키를 눌러 열기";
    public string interactMessage = "안에는 아무것도 없다";
    public InteractionPromptUI promptUI;

    private bool isPlayerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(TagName.Player))
        {
            isPlayerInRange = true;
            promptUI.Show(promptMessage);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(TagName.Player))
        {
            isPlayerInRange = false;
            promptUI.Hide();
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    protected virtual void Interact()
    {
        UIManager.Instance.interactionUI.ShowMessage(interactMessage);
    }
}
