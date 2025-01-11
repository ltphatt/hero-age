using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Dashing")]
    public int dashCost = 10;
    public bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24f;
    public float dashCooldown = 1f;
    public float dashDuration = 0.2f;
    TrailRenderer trailRenderer;

    [Header("Auto-Aim")]
    public int autoAimCost = 20;
    public bool isAutoAiming = false;
    public bool canAutoAim = true;
    public float autoAimCooldown = 3f;
    public float autoAimDuration = 3f;
    public float autoAimRange = 5f;

    [Header("Preferences")]
    PlayerInput gameInput;
    PlayerController playerController;

    Animator animator;

    Rigidbody2D rb;

    [Header("Audio Manager")]
    AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        gameInput = FindObjectOfType<PlayerInput>();
        playerController = GetComponent<PlayerController>();
        trailRenderer = GetComponent<TrailRenderer>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameInput.GetDash() && canDash && playerController.MP >= dashCost)
        {
            // Cost
            playerController.MP -= dashCost;

            // Play sound effect
            audioManager.PlayPlayerMovementSFX(audioManager.dash);

            // Dash
            StartCoroutine(Dash());
        }

        if (gameInput.GetAutoAim() && canAutoAim && playerController.MP >= autoAimCost)
        {
            // Cost
            playerController.MP -= autoAimCost;

            // TODO: Play sound effect for auto aim skill

            // Auto-aim
            StartCoroutine(AutoAim());
        }
    }

    private IEnumerator AutoAim()
    {
        canAutoAim = false;
        isAutoAiming = true;
        animator.SetBool("IsUsingSkill", true);
        Debug.Log("Auto-aiming has started");

        yield return new WaitForSeconds(autoAimDuration);
        isAutoAiming = false;
        animator.SetBool("IsUsingSkill", false);
        Debug.Log("Auto-aiming has ended");

        yield return new WaitForSeconds(autoAimCooldown);
        canAutoAim = true;
        Debug.Log("Auto-aiming is available");
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        // Save original gravity
        float originGravity = rb.gravityScale;

        // Dash
        rb.gravityScale = 0f;
        float direction = transform.rotation.y == 0 ? 1 : -1;
        rb.velocity = new Vector2(direction * dashingPower, 0f);

        // Dash trail
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        trailRenderer.emitting = false;

        // Reset velocity
        rb.gravityScale = originGravity;

        // Cooldown
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
