using UnityEngine;

public class LampCharger : Interactable
{
    public float restoreAmount = 30f;

    private void Reset()
    {
        promptMessage = "E키를 눌러 충전하기";
        interactMessage = "램프가 조금 따뜻해졌다";
    }

    private void Awake()
    {
        if (string.IsNullOrWhiteSpace(promptMessage))
            promptMessage = "E키를 눌러 충전하기";

        if (string.IsNullOrWhiteSpace(interactMessage))
            interactMessage = "램프가 조금 따뜻해졌다";
    }

    protected override void Interact()
    {
        base.Interact();

        LampController lamp = player.GetComponent<LampController>();
        if (lamp != null)
        {
            lamp.RestoreLamp(restoreAmount);
        }

        promptUI.Hide();
        Destroy(gameObject);
    }
}
