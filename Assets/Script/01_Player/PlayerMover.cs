using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("점프 설정")]
    public int maxJumps = 1;
    private int remainingJumps;

    [Header("계단 상태")]
    public bool isOnStair;  // 현재 계단 상태
    public bool isStair;    // 계단 트리거 안에 있는지(토글/컨텍스트용)

    [Header("참조")]
    public GroundChecker2D groundChecker;
    private bool isGrounded;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        remainingJumps = maxJumps;
    }

    private void OnEnable()
    {
        PlayerInputManager.Instance.Jump += OnJumpPressed;
    }

    private void OnDisable()
    {
        PlayerInputManager.Instance.Jump -= OnJumpPressed;
    }
    private void Update()
    {
        if (groundChecker != null) groundChecker.Refresh();
        isGrounded = groundChecker.IsGrounded;

        GroundCheckLogic();
    }
    private void FixedUpdate()
    {
        float x = PlayerInputManager.Instance.GetMoveX();
        ApplyHorizontal(x);
    }

    private void ApplyHorizontal(float x)
    {
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
    }

    private void OnJumpPressed()
    {
        TryJump();
    }

    private void TryJump()
    {
        if (remainingJumps <= 0) return;

        ResetVerticalVelocity();
        AddJumpImpulse(jumpForce);
        remainingJumps--;
    }

    private void ResetVerticalVelocity()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
    }

    private void AddJumpImpulse(float force)
    {
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    private void GroundCheckLogic()
    {
        if (isGrounded && !isOnStair)
            remainingJumps = maxJumps;

        if (isOnStair && !isStair && isGrounded)
            ExitStair();
    }

    private void ExitStair()
    {
        isOnStair = false;
        rb.gravityScale = 5f;

        // 카메라 스위치(기존 조건 유지)
        if (transform.position.y > 10f)
            GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor2);
        else
            GameManager.Instance.cameraGroupController.ActivateCamera(CameraName.Floor1);
    }
}
