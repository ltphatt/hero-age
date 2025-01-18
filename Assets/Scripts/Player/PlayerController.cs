using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private static string IS_WALKING = "IsWalking";

    [Header("Player Properties")]
    public int HP = 5;
    public int maxHP = 5;
    public int MP = 5;
    public int maxMP = 5;
    public int lives = 3;

    public HeartUI heartUI;

    [Tooltip("MP regeneration rate per 5 seconds")]
    public int MPRegenRate = 1;
    [SerializeField] private int coin = 0;
    public int Coin => coin;


    [Header("Preferences")]
    PlayerInput gameInput;
    private PlayerMovement playerMovement;

    private bool isBuffed = false;
    private float buffDuration = 0f;
    private float buffMultiplier = 1f;

    private int originalHP;

    private Animator animator;

    [Header("Player HUD")]
    [SerializeField] private Image healBar;
    [SerializeField] private Image manaBar;

    [Header("Player Sounds")]
    AudioManager audioManager;

    public static event Action OnPlayerDied;

    public static event Action<int> OnPlayerRespawn;
    public static event Action<int> OnPlayerCoinChange;

    private float regenManaTimer = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(IS_WALKING, false);
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        gameInput = FindObjectOfType<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        LoadPlayerData();
    }

    private void Start()
    {
        heartUI.UpdateHearts(lives);
        OnPlayerRespawn?.Invoke(lives);
    }

    private void Update()
    {
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

        regenManaTimer += Time.deltaTime;
        if (regenManaTimer >= 5f)
        {
            RegenerateMana();
            regenManaTimer = 0f;
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
        manaBar.fillAmount = (float)MP / maxMP;
    }

    public void ChangeHealth(int value)
    {
        // Play SFX when taking damage
        if (value < 0)
        {
            audioManager.PlayPlayerSFX(audioManager.hit);
        }
        HP = Mathf.Clamp(HP + value, 0, maxHP);
        UpdateHealthBarUI();

        if (HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (lives > 1)
        {
            lives--;
            OnPlayerRespawn?.Invoke(lives);
        }
        else
        {
            Debug.Log("Player is out of lives. Game Over!");
            if (gameObject != null)
            {
                gameObject.SetActive(false);
                gameInput.Disable();
            }

            OnPlayerDied?.Invoke();
            OnPlayerDied = null;
        }
    }

    public void ChangeCoin(int value)
    {
        coin += value;
        OnPlayerCoinChange?.Invoke(coin);
    }

    public void ChangeMaxHP(int value)
    {
        maxHP += value;
        UpdateHealthBarUI();
    }

    public void ChangeMaxMP(int value)
    {
        maxMP += value;
        UpdateHealthBarUI();
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

    // Take damage from enemy
    public void TakeDamage(int damage)
    {
        ChangeHealth(-damage);
    }

    // Increase player's mana per second
    public void RegenerateMana()
    {
        if (MP < maxMP)
        {
            MP += MPRegenRate;
        }
    }

    public void ResetStats()
    {
        Debug.Log("Player stats reset");
        if (isBuffed)
        {
            RemoveAmuletBuff();
        }

        HP = maxHP;
        MP = maxMP;
    }

    public void SavePlayerData()
    {
        PlayerPrefs.SetInt("MaxHP", maxHP);
        PlayerPrefs.SetInt("MaxMP", maxMP);
        PlayerPrefs.SetInt("Coin", coin);
        PlayerPrefs.SetInt("HP", HP);
        PlayerPrefs.SetInt("MP", MP);
        PlayerPrefs.SetInt("Lives", lives);
    }

    public void LoadPlayerData()
    {
        maxHP = PlayerPrefs.GetInt("MaxHP", maxHP);
        coin = PlayerPrefs.GetInt("Coin", 0);
        maxMP = PlayerPrefs.GetInt("MaxMP", maxMP);
        HP = PlayerPrefs.GetInt("HP", maxHP);
        MP = PlayerPrefs.GetInt("MP", maxMP);
        lives = PlayerPrefs.GetInt("Lives", lives);

        UpdateHealthBarUI();
    }

    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteKey("MaxHP");
        PlayerPrefs.DeleteKey("MaxMP");
        PlayerPrefs.DeleteKey("Coin");
        PlayerPrefs.DeleteKey("HP");
        PlayerPrefs.DeleteKey("MP");
        PlayerPrefs.DeleteKey("Lives");
        LoadPlayerData();
    }
}
