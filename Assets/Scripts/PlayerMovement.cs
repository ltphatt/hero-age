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
        isJumping = rb.velocity.y != 0;
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
    }
}
