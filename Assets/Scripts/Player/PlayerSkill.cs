using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [Header("Dashing")]
    public bool canDash = true;
    public bool isDashing;
    public float dashingPower = 24f;
    public float dashCooldown = 1f;
    public float dashDuration = 0.2f;
    public TrailRenderer trailRenderer;

    [Header("Player Properties")]
    [SerializeField] PlayerInput gameInput;
    Rigidbody2D rb;

    [Header("Audio Manager")]
    AudioManager audioManager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {

        if (gameInput.GetDash() && canDash)
        {
            audioManager.PlayPlayerMovementSFX(audioManager.dash);
            StartCoroutine(Dash());
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
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
