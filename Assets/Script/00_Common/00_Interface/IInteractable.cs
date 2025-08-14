using System;
using UnityEngine;

public interface IInteractable
{
    void Interact(GameObject interactor);

    event Action<string> Interacted;
}
