using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static string IS_WALKING = "IsWalking";
    private static string FIRE = "Fire";

    [Header("Player Properties")]
    [SerializeField] private int HP = 5;
    [SerializeField] private int maxHP = 5;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] PlayerInput gameInput;
    [SerializeField] private PlayerMovement playerMovement;
    private Animator animator;

    [Header("Player HUD")]
    [SerializeField] private Image healBar;


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

        UpdateHealthBarUI();
    }

    void UpdateHealthBarUI()
    {
        healBar.fillAmount = (float)HP / maxHP;
    }

    void Fire()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firePoint.position, transform.rotation);
    }

    public void ChangeHealth(int value)
    {
        HP = Mathf.Clamp(HP + value, 0, maxHP);
        Debug.Log("Current HP: " + HP + "/" + maxHP);
    }
}
