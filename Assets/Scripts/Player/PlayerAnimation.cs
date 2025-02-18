using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private static string IS_WALKING = "IsWalking";
    [SerializeField] private PlayerMovement playerMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, false);
    }

    private void Start()
    {
        animator.ResetTrigger("Dead");
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, playerMovement.IsWalking());

    }
}
