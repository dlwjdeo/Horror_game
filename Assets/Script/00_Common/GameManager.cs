using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    public Player player;
    public CameraGroupController cameraGroupController;

    public Language Language = Language.Kr;

    private bool isInputBlocked = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool BlockInput()
    {
        isInputBlocked = true;
        return isInputBlocked;
    }

    public bool UnblockInput()
    {
        isInputBlocked = false;
        return isInputBlocked;
    }

    public bool GetInputBlockState()
    {
        return isInputBlocked;
    }
}
