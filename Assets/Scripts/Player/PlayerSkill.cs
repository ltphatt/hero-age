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

    [Header("Tornado")]
    public int tornadoCost = 30;
    public bool canUseTornado = true;
    public float tornadoCooldown = 5f;
    [SerializeField] private GameObject tornadoPrefab;

    [Header("Tiger")]
    public int tigerCost = 20;
    public bool canUseTiger = true;
    public float tigerCooldown = 5f;
    [SerializeField] private GameObject tigerPrefab;

    [Header("Dragon")]
    public int dragonCost = 30;
    public bool canUseDragon = true;
    public float dragonCooldown = 5f;
    [SerializeField] private GameObject dragonPrefab;


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
        if (gameInput.GetInputSkill(SkillType.DASH) && canDash && playerController.MP >= dashCost)
        {
            // Cost
            playerController.MP -= dashCost;

            // Play sound effect
            audioManager.PlayPlayerMovementSFX(audioManager.dash);

            // Dash
            StartCoroutine(Dash());
        }

        if (gameInput.GetInputSkill(SkillType.AUTO_AIM) && canAutoAim && playerController.MP >= autoAimCost)
        {
            // Cost
            playerController.MP -= autoAimCost;

            // TODO: Play sound effect for auto aim skill, loop: once

            // Auto-aim
            StartCoroutine(AutoAim());
        }

        if (gameInput.GetInputSkill(SkillType.TORNADO) && canUseTornado && playerController.MP >= tornadoCost)
        {
            // Cost
            playerController.MP -= tornadoCost;

            // TODO: Play sound effect for tornado skill, loop: once

            // Tornado
            StartCoroutine(Tornado());
        }

        if (gameInput.GetInputSkill(SkillType.TIGER) && canUseTiger && playerController.MP >= tigerCost)
        {
            // Cost
            playerController.MP -= tigerCost;

            //TODO: Play sound effect for tiger skill, loop: once

            // Tiger skill
            StartCoroutine(Tiger());
        }

        if (gameInput.GetInputSkill(SkillType.DRAGON) && canUseDragon && playerController.MP >= dragonCost)
        {
            // Cost
            playerController.MP -= dragonCost;

            // TODO: Play sound effect for dragon skill, loop: once

            // Use dragon skill
            StartCoroutine(Dragon());
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

    private IEnumerator Tornado()
    {
        canUseTornado = false;

        GameObject tornadoObject = Instantiate(tornadoPrefab, transform.position + new Vector3(0, 1.75f, 0), Quaternion.identity);

        yield return new WaitForSeconds(tornadoCooldown);
        canUseTornado = true;
    }

    private IEnumerator Tiger()
    {
        canUseTiger = false;

        GameObject tigerObject = Instantiate(tigerPrefab, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);

        yield return new WaitForSeconds(tigerCooldown);
        canUseTiger = true;
    }

    private IEnumerator Dragon()
    {
        canUseDragon = false;

        GameObject dragonObject = Instantiate(dragonPrefab, transform.position + new Vector3(0, 1.25f, 0), Quaternion.identity);

        yield return new WaitForSeconds(dragonCooldown);
        canUseDragon = true;
    }
}
