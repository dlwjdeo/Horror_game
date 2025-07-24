using UnityEngine;

public class Chest : Interactable
{
    private void Reset()
    {
        promptMessage = "E키를 눌러 상자를 열기";
        interactMessage = "상자 안에는 먼지만 가득하다";
    }
    private void Awake()
    {
        if (string.IsNullOrWhiteSpace(promptMessage))
            promptMessage = "E키를 눌러 상자를 열기";

        if (string.IsNullOrWhiteSpace(interactMessage))
            interactMessage = "상자 안에는 먼지만 가득하다";
    }

    protected override void Interact()
    {
        base.Interact();

        promptUI.Hide();
        Destroy(gameObject);
    }
}
