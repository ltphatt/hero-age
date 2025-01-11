using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private static string FIRE = "Fire";

    [Header("Player Attack")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    PlayerInput gameInput;
    private PlayerMovement playerMovement;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gameInput = FindObjectOfType<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (gameInput.GetFire())
        {
            animator.SetTrigger(FIRE);
            Fire();
        }
    }

    void Fire()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
    }
}
