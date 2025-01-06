using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static string IS_WALKING = "IsWalking";
    private static string FIRE = "Fire";

    [Header("Player Properties")]
    [SerializeField] private int HP = 5;
    [SerializeField] private int maxHP = 5;
    [SerializeField] private int coin = 0;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] PlayerInput gameInput;
    [SerializeField] private PlayerMovement playerMovement;

    private bool isBuffed = false;
    private float buffDuration = 0f;
    private float buffMultiplier = 1f;

    private int originalHP;

    private Animator animator;

    [Header("Player HUD")]
    [SerializeField] private Image healBar;

    AudioSource audioSource;

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

        if (isBuffed)
        {
            buffDuration -= Time.deltaTime;
            if (buffDuration <= 0)
            {
                Debug.Log("Buff duration has ended");
                RemoveAmuletBuff();
            }
        }

        UpdateHealthBarUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Heal"))
        {
            HealController healController = collision.GetComponent<HealController>();
            healController.HealPlayer(this);
            healController.DestroySelf();
        }
        else if (collision.CompareTag("Coin"))
        {
            CoinController coinController = collision.GetComponent<CoinController>();
            coinController.CollectCoin(this);
            coinController.DestroySelf();
        }
        else if (collision.CompareTag("Amulet"))
        {
            AmuletController amuletController = collision.GetComponent<AmuletController>();
            amuletController.CollectBuff(this);
            amuletController.DestroySelf();
        }
        {
            // Do nothing
        }
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

    public void ChangeCoin(int value)
    {
        coin += value;
        Debug.Log("Current Coin: " + coin);
    }

    public void ApplyAmuletBuff(float duration, int multiplier)
    {
        if (isBuffed)
        {
            RemoveAmuletBuff();
        }

        isBuffed = true;
        buffDuration = duration;
        buffMultiplier = multiplier;
        Debug.Log("Player is buffed for " + duration + " seconds with multiplier " + multiplier);

        originalHP = maxHP;

        maxHP = (int)(maxHP * multiplier);

        int buffHP = maxHP - originalHP;
        HP += buffHP;

        Debug.Log("Current HP: " + HP + "/" + maxHP);
    }

    public void RemoveAmuletBuff()
    {
        isBuffed = false;
        maxHP = (int)originalHP;
        HP = Mathf.Clamp(HP, 0, maxHP);
        Debug.Log("Player is no longer buffed");

        Debug.Log("Current HP: " + HP + "/" + maxHP);
    }
}
