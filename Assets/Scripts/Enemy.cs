using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private bool veritcalMovement = true;
    [SerializeField] private float idleDuration = 3f;
    [SerializeField] private float moveDuration = 3f;
    Rigidbody2D rb;
    float timer = 0f;
    float idleTimer = 0f;

    private float direction = 1f;
    Animator animator;
    private bool isWalking = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        isWalking = true;
        idleTimer = idleDuration;
        timer = moveDuration;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            isWalking = false;

            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0f)
            {
                direction = direction * -1;

                isWalking = true;
                timer = moveDuration;
                idleTimer = idleDuration;
            }
        }

        animator.SetBool("IsWalking", isWalking);
    }

    private void FixedUpdate()
    {
        HandleEnemyMovement();
    }

    void HandleEnemyMovement()
    {
        if (!isWalking) return;

        Vector2 pos = rb.position;
        if (veritcalMovement)
        {
            pos.y += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            pos.x += direction * moveSpeed * Time.deltaTime;
        }

        rb.MovePosition(pos);

        FlipSprite(-direction);
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Face right
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Face left
        }
    }
}
