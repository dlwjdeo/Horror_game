// PlayerInputManager.cs
using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    private Vector2 _move;

    public event Action Jump;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        ReadMove();
        DetectJumpPress();
    }

    // --- 입력 읽기 (함수화) ---
    public void ReadMove()
    {
        _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public Vector2 GetMove() => _move;
    public float GetMoveX() => _move.x;
    public bool IsMovingHorizontally() => Mathf.Abs(_move.x) > 0.01f;

    private void DetectJumpPress()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Jump?.Invoke();
    }
}
