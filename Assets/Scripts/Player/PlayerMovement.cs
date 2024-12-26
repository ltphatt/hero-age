using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerInput gameInput;
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float slowWhenJump = 0.6f;
    private bool isJumping = false;
    private bool isWalking = false;
    Rigidbody2D rb;

    [Header("Dashing")]
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing;
    [SerializeField] float dashingPower = 24f;
    [SerializeField] float dashCooldown = 1f;
    [SerializeField] float dashDuration = 0.2f;
    [SerializeField] TrailRenderer trailRenderer;

    private void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDashing)
        {
            return;
        }

        HandleMovement();
        HandleJump();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
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

    private void HandleJump()
    {
        isJumping = !rb.IsTouchingLayers(LayerMask.GetMask("Ground"));

        if (gameInput.GetJump() && !isJumping)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void HandleMovement()
    {
        var playerMoveSpeed = isJumping ? moveSpeed * slowWhenJump : moveSpeed;

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, 0f);

        transform.position += moveDir * playerMoveSpeed * Time.deltaTime;

        isWalking = moveDir != Vector3.zero;

        FlipSprite(inputVector.x);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void FlipSprite(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        trailRenderer.emitting = false;
        rb.gravityScale = originGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
