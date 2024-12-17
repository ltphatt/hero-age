using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static string IS_WALKING = "IsWalking";
    private static string FIRE = "Fire";

    [SerializeField] private int HP = 5;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] GameInput gameInput;
    [SerializeField] private PlayerMovement playerMovement;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, false);
    }

    private void Update()
    {
        if (gameInput.GetFire())
        {
            animator.SetTrigger(FIRE);
            Fire();
        }

        animator.SetBool(IS_WALKING, playerMovement.IsWalking());
    }

    void Fire()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
    }
}
