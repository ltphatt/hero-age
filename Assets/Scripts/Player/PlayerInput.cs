using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVector()
    {
        Vector2 inputVector = new Vector2(0, 0);
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }

    public bool GetJump()
    {
        bool jump = playerInputActions.Player.Jump.triggered;
        return jump;
    }

    public bool GetFire()
    {
        bool fire = playerInputActions.Player.Fire.triggered;
        return fire;
    }
}
