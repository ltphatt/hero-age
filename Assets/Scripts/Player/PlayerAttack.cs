using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private static string FIRE = "Fire";

    [Header("Player Attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject skillProjectilePrefab;
    [SerializeField] Transform firePoint;
    PlayerInput gameInput;
    private Animator animator;
    PlayerSkill playerSkill;
    private Transform target = null;
    AudioManager audioManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameInput = FindObjectOfType<PlayerInput>();
        playerSkill = GetComponent<PlayerSkill>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        FindTarget(transform.position, playerSkill.autoAimRange);

        if (gameInput.GetFire())
        {
            animator.SetTrigger(FIRE);

            var isAutoAiming = playerSkill.isAutoAiming;
            if (isAutoAiming)
            {
                // Fire skill projectile
                audioManager.PlayPlayerAttackSFX(audioManager.hoodSkillProjectile);
                FireSkillProjectile();
            }
            else
            {
                // Normal fire
                audioManager.PlayPlayerAttackSFX(audioManager.hoodProjectile);
                Fire();
            }
        }
    }

    void Fire()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
    }

    void FireSkillProjectile()
    {
        GameObject skillProjectileObject = Instantiate(skillProjectilePrefab, firePoint.position, transform.rotation);
        HoodSkillProjectile skillProjectile = skillProjectileObject.GetComponent<HoodSkillProjectile>();
        skillProjectile.SetTarget(target);
    }

    public void FindTarget(Vector2 center, float radius)
    {
        Collider2D hit = Physics2D.OverlapCircle(center, radius, LayerMask.GetMask("Enemy"));
        if (hit != null)
        {
            target = hit.transform;
        }
    }
}