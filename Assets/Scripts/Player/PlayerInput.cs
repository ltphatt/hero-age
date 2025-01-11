using UnityEngine;

public enum SkillType
{
    DASH,
    AUTO_AIM,
    TORNADO,
    TIGER
}

public class PlayerInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Player.Disable();
    }

    private void OnEnable()
    {
        playerInputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        playerInputActions.Player.Disable();
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

    public void Disable()
    {
        playerInputActions.Player.Disable();
    }

    public bool GetInputSkill(SkillType skillType)
    {
        bool result = false;
        switch (skillType)
        {
            case SkillType.DASH:
                result = playerInputActions.Player.Dash.triggered;
                break;
            case SkillType.AUTO_AIM:
                result = playerInputActions.Player.AutoAim.triggered;
                break;
            case SkillType.TORNADO:
                result = playerInputActions.Player.Tornado.triggered;
                break;
            case SkillType.TIGER:
                result = playerInputActions.Player.Tiger.triggered;
                break;
            default:
                return false;
        }

        return result;
    }
}
