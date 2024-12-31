using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 20;
    public int maxHealth = 20;
    public int enragedHealth = 10;
    Animator animator;
    [SerializeField] GameObject bossDeathffect;
    SpriteRenderer spriteRenderer;
    bool isEnraged = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");

        if (health == maxHealth / 2 && !isEnraged)
        {
            isEnraged = true;
            spriteRenderer.color = new Color(255f / 255f, 155f / 255f, 155f / 255f, 255f / 255f);
            health += enragedHealth;
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(bossDeathffect, transform.position + Vector3.up, transform.rotation);
        Destroy(gameObject);
    }
}
