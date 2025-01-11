using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSkill : MonoBehaviour
{
    Rigidbody2D rb;
    float timer = 0f;
    [SerializeField] float speed = 5f;
    [SerializeField] float timeLife = 1f;
    [SerializeField] int damage = 5;
    PlayerController player;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
        player = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        speed = player.transform.forward.z * speed;

        FlipSprite();
    }

    private void Update()
    {
        rb.velocity = new Vector2(speed, 0f);

        timer += Time.deltaTime;
        if (timer > timeLife)
        {
            Destroy(gameObject);
            timer = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.ChangeHealth(-damage);
            enemy.ChangeToInCombatState();
        }

        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }
    }

    private void FlipSprite()
    {
        if (speed < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
