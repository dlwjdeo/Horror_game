using System;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }

    // 캐시
    private float moveX, moveY;
    private bool jumpDown, interactDown, dropDown;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }
    //인풋 담당
    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        jumpDown = Input.GetKeyDown(KeyCode.Space);
        interactDown = Input.GetKeyDown(KeyCode.E);
        dropDown = Input.GetKeyDown(KeyCode.Q); //아이템 드롭

    }

    // ----- 외부 API (폴링) -----
    public float MoveX() => moveX;
    public float MoveY() => moveY;

    public Vector2 Move() => new Vector2(moveX, moveY);

    public bool JumpPressedThisFrame() => jumpDown;
    public bool InteractPressedThisFrame() => interactDown;
    public bool DropPressedThisFrame() => dropDown;


}