using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoodSkillProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] float timeLife = 5f;
    private Transform target = null;
    float timer = 0f;
    Rigidbody2D rb;
    PlayerController player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (target != null)
        {
            RotateFollowTarget();
            Vector2 direction = (target.position - transform.position + Vector3.up).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            speed = player.transform.forward.z * speed;
            rb.velocity = new Vector2(speed, 0f);
        }

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

            Destroy(gameObject);
        }

        BossHealth boss = other.GetComponent<BossHealth>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void RotateFollowTarget()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
