using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public enum State
    {
        InCombat,
        OutCombat,
        BeStunned,
        BeBurned,
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

    [Header("Effects")]
    [SerializeField] GameObject enemyDeathPrefab;
    [SerializeField] GameObject iceStunObject;
    private float stunDuration = 1f;
    private float stunTimer = 0f;
    ParticleSystem burnEffect;
    private float burnTimer = 0f;
    private float subTimer = 0f;
    private int burnDamage = 1;
    private float burnDuration = 5f;

    [Header("Audio Manager")]
    AudioManager audioManager;

    [Header("Enemy Type")]
    [SerializeField] private string enemyType;

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

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        iceStunObject.SetActive(false);

        burnEffect = GetComponentInChildren<ParticleSystem>();
    }

    // private void OnDrawGizmosSelected()
    // {
    //     Handles.color = Color.green;
    //     Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    // }

    private void Start()
    {
        burnEffect.Stop();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        // audioSource = GetComponent<AudioSource>();

        isWalking = true;
        idleTimer = idleDuration;
        timer = moveDuration;

        state = State.OutCombat;
    }

    private void Update()
    {
        if (state == State.BeStunned)
        {
            return;
        }

        HandleEnemyDirection();

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

        CheckIsAlive();
    }

    private void FixedUpdate()
    {
        if (state == State.BeStunned)
        {
            HandleEnemyBeStunned();
        }

        if (state == State.BeBurned)
        {
            HandleEnemyBeBurned();
        }

        HandleEnemyMovement();
        HandleEnemyState();
    }

    void HandleEnemyMovement()
    {
        if (!isWalking || state == State.BeStunned)
        {
            return;
        }

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

    private void HandleEnemyDirection()
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
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        bullet.SetTarget(target);
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
        // PlayHitSound();
        audioManager.PlayEnemySFX(enemyType, gameObject);
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

    private void CheckIsAlive()
    {
        if (enemyHP <= 0)
        {
            audioManager.PlayEnemyDeathSFX();
            Instantiate(enemyDeathPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Stunned(float duration)
    {
        stunDuration = duration;
        iceStunObject.SetActive(true);
        state = State.BeStunned;
    }

    private void HandleEnemyBeStunned()
    {
        IceBroken iceBroken = iceStunObject.GetComponent<IceBroken>();

        if (stunTimer >= stunDuration - 0.5f)
        {
            iceBroken.Broken();
        }

        stunTimer += Time.deltaTime;
        if (stunTimer >= stunDuration)
        {
            iceStunObject.SetActive(false);
            state = State.InCombat;
            stunTimer = 0f;
            iceBroken.ResetBroken();
        }
    }

    public void Burn(int damage, float duration)
    {
        Debug.Log("Start Burn");
        if (burnEffect != null)
        {
            burnEffect.Play();
        }

        burnDamage = damage;
        burnDuration = duration;

        state = State.BeBurned;
    }

    private void HandleEnemyBeBurned()
    {
        burnTimer += Time.deltaTime;
        subTimer += Time.deltaTime;

        if (subTimer >= 1f)
        {
            ChangeHealth(-burnDamage);
            Debug.Log("Burned");
            subTimer = 0f;
        }

        if (burnTimer >= burnDuration)
        {
            burnEffect.Stop();
            state = State.InCombat;
            burnTimer = 0f;

            Debug.Log("End Burn");
        }
    }
}
