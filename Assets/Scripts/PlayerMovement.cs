using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float slowWhenJump = 0.6f;
    private bool isJumping = false;
    private bool isWalking = false;
    Rigidbody2D rb;

    private void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleJump()
    {
        isJumping = !rb.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void HandleMovement()
    {
        var currentSpeed = isJumping ? (moveSpeed * slowWhenJump) : moveSpeed;
        float horizontal = Input.GetAxis("Horizontal");

        Vector2 pos = transform.position;
        pos.x += horizontal * currentSpeed * Time.deltaTime;
        transform.position = pos;

        isWalking = (horizontal != 0) && !isJumping;

        FlipSprite(horizontal);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void FlipSprite(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face right
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Face left
        }
    }
}
