using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampController : MonoBehaviour
{
    [SerializeField] private LampGauge lampGauge;
    [SerializeField] private Light2D lampLight;


    private void Awake()
    {
        lampLight.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleLamp();
        }

        if (lampGauge.isOn && Mathf.Approximately(lampGauge.currentLamp, 0f))
        {
            ForceTurnOffLamp();
        }

    }

    private void ToggleLamp()
    {
        bool isOn = lampGauge.Toggle();
        lampLight.enabled = isOn;
    }

    private void ForceTurnOffLamp()
    {
        if (lampGauge.isOn)
        {
            lampGauge.ForceOff();
            lampLight.enabled = false;
            Debug.Log("게이지가 0이 되어 램프를 강제로 껐습니다.");
        }
    }
    public void RestoreLamp(float amount)
    {
        lampGauge.AddLamp(amount);
    }
}
