using System;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        InCombat,
        OutCombat,
    }

    [Header("Enemy movement")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private bool veritcalMovement = true;
    [SerializeField] private float idleDuration = 3f;
    [SerializeField] private float moveDuration = 3f;

    [Header("Attack Player")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float bulletPerSec = 2f;

    [Header("Enemy Properties")]
    [SerializeField] private int enemyHP = 5;
    [SerializeField] private int enemyMaxHP = 5;
    [SerializeField] private State state;


    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    float timer = 0f;
    float idleTimer = 0f;

    private float direction = 1f;
    Animator animator;
    private bool isWalking = true;
    private Transform target = null;
    private float timeUntilFire;
    float inCombatTimer = 0f;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.green;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        isWalking = true;
        idleTimer = idleDuration;
        timer = moveDuration;

        state = State.OutCombat;
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
        HandleEnemyState();
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
            spriteRenderer.flipX = false;
        }
        else if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void AttackPlayer()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, transform.position, transform.rotation);
        DinoBullet bullet = bulletObj.GetComponent<DinoBullet>();
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

    public void ChangeHealth(int value)
    {
        enemyHP = Mathf.Clamp(enemyHP + value, 0, enemyMaxHP);
    }

    public int GetMaxHP()
    {
        return enemyMaxHP;
    }

    public int GetHP()
    {
        return enemyHP;
    }

    private void HandleEnemyState()
    {
        if (state == State.InCombat)
        {
            inCombatTimer += Time.deltaTime;
            Debug.Log("In Combat timer: " + inCombatTimer);
        }

        if (inCombatTimer >= 5f)
        {
            state = State.OutCombat;
            inCombatTimer = 0f;
        }
    }

    public State GetState()
    {
        return state;
    }

    public void ChangeToInCombatState()
    {
        state = State.InCombat;
        inCombatTimer = 0f;
    }
}
