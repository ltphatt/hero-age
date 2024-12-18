using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float slowWhenJump = 0.6f;
    [SerializeField] PlayerInput gameInput;
    private bool isJumping = false;
    private bool isWalking = false;
    Rigidbody2D rb;


    private void Start()
    {
        isJumping = false;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
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
}
