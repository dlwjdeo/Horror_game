using UnityEngine;
using UnityEngine.UI;

public class LampGauge : MonoBehaviour
{
    [Header("램프 게이지 설정")]
    public Slider lampSlider;
    public float maxLamp = 100f;
    public float currentLamp = 100f;
    public float decayRate = 1f;
    public bool isOn = false;


    private void Awake()
    {
        maxLamp = currentLamp;
        UpdateLampGauge();
    }

    private void Update()
    {
        if (isOn)
        {
           DecayLampOverTime();
        }
    }

    public void AddLamp(float amount)
    {
        float before = currentLamp;
        currentLamp = Mathf.Clamp(currentLamp + amount, 0f, maxLamp);
        if (!Mathf.Approximately(currentLamp, before))
            UpdateLampGauge();
    }

    private void DecayLampOverTime()
    {
        float before = currentLamp;
        currentLamp = Mathf.Clamp(currentLamp - (decayRate * Time.deltaTime), 0f, maxLamp);
        if (!Mathf.Approximately(currentLamp, before))
            UpdateLampGauge();
    }


    private void UpdateLampGauge()
    {
        lampSlider.value = currentLamp / maxLamp;
    }

    public bool Toggle()
    {
        isOn = !isOn;
        return isOn;
    }
    public void ForceOff()
    {
        isOn = false;
    }

    public float GetCurrentLamp() {  return currentLamp; }
}
