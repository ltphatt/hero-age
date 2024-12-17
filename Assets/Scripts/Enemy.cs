using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private bool veritcalMovement = true;
    [SerializeField] private float idleDuration = 3f;
    [SerializeField] private float moveDuration = 3f;

    [Header("Attack Player")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bulletPerSec = 2f;

    Rigidbody2D rb;
    float timer = 0f;
    float idleTimer = 0f;

    private float direction = 1f;
    Animator animator;
    private bool isWalking = true;
    private Transform target = null;
    private float timeUntilFire;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

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

        if (target == null)
        {
            FindPlayer(transform.position, targetingRange);
        }

        if (!CheckPlayerIsInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= (1f / bulletPerSec))
            {
                AttackPlayer();
                timeUntilFire = 0f;
            }
        }
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

    public void AttackPlayer()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, transform.rotation);
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetTarget(target);

        Debug.Log("Attacking player");
    }

    public void FindPlayer(Vector2 center, float radius)
    {
        Collider2D hit = Physics2D.OverlapCircle(center, radius, LayerMask.GetMask("Player"));
        if (hit != null)
        {
            target = hit.transform;
        }
    }

    bool CheckPlayerIsInRange()
    {
        if (target != null)
        {
            return Vector2.Distance(target.position, transform.position) <= targetingRange;
        }
        else
        {
            return false;
        }
    }
}
