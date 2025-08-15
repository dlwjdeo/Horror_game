using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public InteractionUI interactionUI;
    public LampGauge lampGauge;
    public HeldItemUI heldItemUI;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInteractUI(string message)
    {
        interactionUI.ShowMessage(message);
    }

    public void ChargeLamp(float amount)
    {
        lampGauge.AddLamp(amount);
    }
}
