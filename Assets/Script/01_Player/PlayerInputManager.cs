using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    private Vector2 _move;

    public event Action Jump;

    public bool isInStairTrigger = false;

    private void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    private void Update()
    {
        _move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }
    }

    public Vector2 GetMove() => _move;
    public float GetMoveX() => _move.x;
    public float GetMoveY() => _move.y;

    public void SetStairTrigger(bool value) => isInStairTrigger = value;
}