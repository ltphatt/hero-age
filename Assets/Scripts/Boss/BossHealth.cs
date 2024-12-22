using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 10;
    public int maxHealth = 10;
    Animator animator;
    [SerializeField] GameObject bossDeathffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("Hurt");

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
