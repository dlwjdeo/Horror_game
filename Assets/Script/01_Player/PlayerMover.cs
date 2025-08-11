using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 5f;
    public float jumpForce = 6f;

    [Header("점프 설정")]
    public int maxJumps = 1;
    private int remainingJumps;

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
}
