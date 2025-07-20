using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomLightController : MonoBehaviour
{
    public Light2D roomLight;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RoomManager.Instance.SwitchRoom(this);
        }
    }

    public void SetLightActive(bool active)
    {
        if (roomLight != null)
            roomLight.enabled = active;
    }
}
