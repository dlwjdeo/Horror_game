using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraGroupController : MonoBehaviour
{
    private CinemachineVirtualCamera[] cams;

    private void Awake()
    {
        cams = GetComponentsInChildren<CinemachineVirtualCamera>(true);
    }

    public void ActivateCamera(string cameraName)
    {
        foreach (var cam in cams)
        {
            cam.Priority = cam.name == cameraName ? 20 : 10;
        }
    }

    public void ActivateCamera(CinemachineVirtualCamera target)
    {
        foreach (var cam in cams)
        {
            cam.Priority = (cam == target) ? 20 : 10;
        }
    }
}
