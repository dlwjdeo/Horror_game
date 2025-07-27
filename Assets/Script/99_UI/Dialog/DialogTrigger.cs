using UnityEngine;

public class DialogTrigger : Interactable
{
    public DialogName dialogName;

    private void Reset()
    {
        promptMessage = "E키를 눌러 대화하기";
        interactMessage = "";//Dialog에서는 따로 Dialog를 출력하기 때문에 메시지 제거
    }
    private void Awake()
    {
        if (string.IsNullOrWhiteSpace(promptMessage))
            promptMessage = "E키를 눌러 상자를 열기";
    }

    protected override void Interact()
    {
        promptUI.Hide();
        DialogManager.Instance.StartDialog(dialogName);
    }

}