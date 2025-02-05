using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5.0f;

    private Vector2 moveInput;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    private void Start()
    {
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }

        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;
        rb.linearVelocity = moveDirection * moveSpeed;
        updateAnimation(moveDirection);
    }

    private void updateAnimation(Vector2 moveDirection)
    {
        bool isMoving = moveDirection.magnitude > 0.01f;
        anim.SetBool("isMove", isMoving);
        anim.SetFloat("moveX", moveDirection.x);
        anim.SetFloat("moveY", moveDirection.y);
        
        if (moveDirection.x < 0)
        {
            anim.SetBool("isFacingLeft", true);
        }
        else if (moveDirection.x > 0)
        {
            anim.SetBool("isFacingLeft", false);
        }
    }
}