using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header(">>>>> Audio Source Music")]
    [SerializeField] AudioSource musicSource; // Background music
    [Header(">>>>> Audio Source SFX")]
    [SerializeField] AudioSource SFXSource; // Jerry, Gem, Amulet, BigGem, Checkpoint
    [Header(">>>>> Audio Player Sources")]
    [SerializeField] AudioSource playerMovementSource; // Movement, Jump, Dash
    [SerializeField] AudioSource playerAttackSource; // Attack
    [SerializeField] AudioSource playerSource; // Hit
    [Header(">>>>> Audio Enemy Sources")]
    [SerializeField] AudioSource enemySource; // Enemy hit


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
    [Header(">>>>> Audio Item SFX")]
    public AudioClip checkpoint;
    public AudioClip jerry;
    public AudioClip gem;
    public AudioClip amulet;
    public AudioClip bigGem;
    [Header(">>>>> Audio Enemy Hit")]
    [SerializeField] public List<EnemyAudio> enemyAudios;
    private Dictionary<string, AudioClip> enemyAudioDictionary;
    public float masterVolume = 1f;

    // Instance
    public static AudioManager instance;

    // Awake function to initialize the dictionary
    private void Awake()
    {  // Đảm bảo chỉ có một AudioManager duy nhất
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi chuyển scene
        }
        else
        {
            Destroy(gameObject); // Hủy các bản sao
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

    // Start background music when the game starts
    private void Start()
    {
        Debug.Log(">>>> Start Background Music: " + SceneManager.GetActiveScene().name);
        PlayMusicForScene(SceneManager.GetActiveScene().name);
    }


    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện khi Scene được load
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi không cần thiết
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Phát nhạc nền khi Scene mới được load
        PlayMusicForScene(scene.name);
    }

    // Play the background music for the scene
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

    // Play the SFX function
    public void PlaySFX(AudioClip clip)
    {
        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(clip);
        }
    }

    // Play the SFX function with the enemy type
    public void PlayEnemySFX(string enemyType)
    {
        if (enemyAudioDictionary.TryGetValue(enemyType, out AudioClip audioClip))
        {
            if (!enemySource.isPlaying)
            {
                enemySource.PlayOneShot(audioClip);
            }
        }
        else
        {
            Debug.LogWarning($"No audio found for enemy type: {enemyType}");
        }
    }

    // Play the Player sound effect function
    public void PlayPlayerMovementSFX(AudioClip clip)
    {
        playerMovementSource.PlayOneShot(clip);

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
