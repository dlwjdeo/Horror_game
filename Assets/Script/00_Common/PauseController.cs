using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static PauseController Instance { get; private set; }

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

    public void TryPauseGame()
    {
        Time.timeScale = 0f;
        GameManager.Instance.BlockInput();
    }

    public void TryResumeGame() 
    { 
        Time .timeScale = 1f;
        GameManager.Instance.UnblockInput();
    }
}
