using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header(">>>>> Audio Source Music")]
    [SerializeField] AudioSource musicSource; // Background music

    [Header(">>>>> Audio Source SFX")]
    private Dictionary<GameObject, float> itemHitLastTime;
    [SerializeField] private float itemHitCooldown = 1f;
    [SerializeField] private int itemAudioSourcePoolSize = 5;
    private List<AudioSource> itemAudioSourcePool; // Jerry, Gem, Amulet, BigGem, Checkpoint, Chest, FinishPoint


    [Header(">>>>> Audio Player Sources")]
    [SerializeField] AudioSource playerMovementSource; // Movement, Jump, Dash
    [SerializeField] AudioSource playerAttackSource; // Attack
    [SerializeField] AudioSource playerSource; // Hit
    [SerializeField] AudioSource playerSkillSource; // Skill

    [Header(">>>>> Audio Boss Sources")]
    [SerializeField] AudioSource bossAttackSource;

    [Header(">>>>> Audio Enemy Sources")]

    // Source for enemy hit sound
    [SerializeField] private int enemyAudioSourcePoolSize = 5;
    private List<AudioSource> enemyAudioSourcePool;
    private Dictionary<GameObject, float> enemyHitLastTime;
    [SerializeField] private float enemyHitCooldown = 1f;
    // Source for enemy death sound
    [SerializeField] private int enemyDeathAudioSourcePoolSize = 5;
    private List<AudioSource> enemyDeathAudioSourcePool;

    [Header(">>>>> AudioMixer group")]
    [SerializeField] private AudioMixerGroup enemyMixerGroup;
    [SerializeField] private AudioMixerGroup itemMixerGroup;
    [SerializeField] private AudioMixerGroup enemyDeathMixerGroup;


    [Header(">>>>> Audio Clips Background Music")]
    [SerializeField] public List<SceneMusic> sceneMusicList;
    private Dictionary<string, AudioClip> sceneMusicDictionary;

    [Header(">>>>> Audio Player SFX")]
    public AudioClip death;
    public AudioClip jump;
    public AudioClip dash;
    public AudioClip movement;
    public AudioClip attack;
    public AudioClip hit;
    public AudioClip wallTouch;
    public AudioClip playerArchery;
    public AudioClip autoAim;
    public AudioClip tornado;
    public AudioClip tiger;
    public AudioClip dragon;
    public AudioClip hoodProjectile;
    public AudioClip hoodSkillProjectile;


    [Header(">>>>> Audio Item SFX")]
    public AudioClip finishPoint;
    public AudioClip checkPoint;
    public AudioClip jerry;
    public AudioClip gem;
    public AudioClip amulet;
    public AudioClip bigGem;
    public AudioClip chest;
    public AudioClip crank;

    [Header(">>>>> Audio Enemy SFX")]
    public AudioClip enemyDie;
    [SerializeField] public List<EnemyAudio> enemyAudios;
    private Dictionary<string, AudioClip> enemyAudioDictionary;
    public float masterVolume = 1f;

    [Header(">>>>> Audio Boss SFX")]
    public AudioClip axeAttack;
    public AudioClip swordAttack;
    public AudioClip malletAttack;


    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        enemyHitLastTime = new Dictionary<GameObject, float>();
        itemHitLastTime = new Dictionary<GameObject, float>();
        // Tạo một pool cho item audio source
        itemAudioSourcePool = new List<AudioSource>();
        for (int i = 0; i < itemAudioSourcePoolSize; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = itemMixerGroup;
            audioSource.playOnAwake = false;
            itemAudioSourcePool.Add(audioSource);
        }

        // Tạo một pool cho enemy hit audio source
        enemyAudioSourcePool = new List<AudioSource>();
        for (int i = 0; i < enemyAudioSourcePoolSize; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = enemyMixerGroup;
            audioSource.playOnAwake = false;
            enemyAudioSourcePool.Add(audioSource);
        }

        // Tạo một pool cho enemy death audio source
        enemyDeathAudioSourcePool = new List<AudioSource>();
        for (int i = 0; i < enemyDeathAudioSourcePoolSize; i++)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.outputAudioMixerGroup = enemyDeathMixerGroup;
            audioSource.playOnAwake = false;
            enemyDeathAudioSourcePool.Add(audioSource);
        }


        // Tạo một dictionary để lưu trữ các audio clip cho enemy
        enemyAudioDictionary = new Dictionary<string, AudioClip>();
        foreach (EnemyAudio enemyAudio in enemyAudios)
        {
            if (!enemyAudioDictionary.ContainsKey(enemyAudio.enemyType))
            {
                enemyAudioDictionary.Add(enemyAudio.enemyType, enemyAudio.audioClip);
            }
        }

        // Tạo một dictionary để lưu trữ các audio clip cho background music
        sceneMusicDictionary = new Dictionary<string, AudioClip>();
        foreach (SceneMusic sceneMusic in sceneMusicList)
        {
            if (!sceneMusicDictionary.ContainsKey(sceneMusic.sceneName))
            {
                sceneMusicDictionary.Add(sceneMusic.sceneName, sceneMusic.audioClip);
            }
        }
    }

    private void Start()
    {
        Debug.Log(">>>> Start Background Music: " + SceneManager.GetActiveScene().name);
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        if (sceneMusicDictionary.TryGetValue(sceneName, out AudioClip clip))
        {
            if (musicSource.clip != clip)
            {
                musicSource.clip = clip;
                musicSource.Play();
            }
        }
        else
        {
            Debug.LogWarning($"No music found for scene: {sceneName}");
        }
    }

    public void PlaySFX(AudioClip clip, GameObject obj)
    {
        if (itemHitLastTime.TryGetValue(obj, out float lastTimeHit))
        {
            if (Time.time - lastTimeHit < itemHitCooldown)
            {
                return;
            }
        }

        itemHitLastTime[obj] = Time.time;

        AudioSource availableAudioSource = itemAudioSourcePool.Find(source => !source.isPlaying);
        if (availableAudioSource != null)
        {
            availableAudioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("No available audio source for item hit sound");
        }
    }

    public void PlayEnemySFX(string enemyType, GameObject enemy)
    {
        if (enemyAudioDictionary.TryGetValue(enemyType, out AudioClip audioClip))
        {
            if (enemyHitLastTime.TryGetValue(enemy, out float lastTimeHit))
            {
                if (Time.time - lastTimeHit < enemyHitCooldown)
                {
                    return;
                }
            }

            enemyHitLastTime[enemy] = Time.time;

            AudioSource availableAudioSource = enemyAudioSourcePool.Find(source => !source.isPlaying);
            if (availableAudioSource != null)
            {
                availableAudioSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.LogWarning("No available audio source for enemy hit sound");
            }
        }
        else
        {
            Debug.LogWarning($"No audio found for enemy type: {enemyType}");
        }
    }

    public void PlayEnemyDeathSFX()
    {
        AudioSource availableAudioSource = enemyDeathAudioSourcePool.Find(source => !source.isPlaying);
        if (availableAudioSource != null)
        {
            availableAudioSource.PlayOneShot(enemyDie);
        }
        else
        {
            Debug.LogWarning("No available audio source for enemy death sound");
        }
    }


    // Play the Player sound effect function
    public void PlayPlayerMovementSFX(AudioClip clip)
    {
        playerMovementSource.PlayOneShot(clip);
    }

    public void PlayPlayerSkillSFX(AudioClip clip)
    {
        playerSkillSource.PlayOneShot(clip);
    }

    public void PlayPlayerAttackSFX(AudioClip clip)
    {
        playerAttackSource.PlayOneShot(clip);
    }

    // Play the player sound when getting hit
    public void PlayPlayerSFX(AudioClip clip)
    {
        if (!playerSource.isPlaying)
        {
            playerSource.PlayOneShot(hit);
        }
    }
}
