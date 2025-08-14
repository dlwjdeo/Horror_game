public interface IPromptSource
{
    InteractionPromptUI interactionPromptUI { get; }
    string promptText { get; }
    void ShowMessage(string message);
    void HideMessage();
}
