using System.Collections;
using System.Collections.Generic;
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
    public AudioClip background;
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
        enemyAudioDictionary = new Dictionary<string, AudioClip>();
        foreach (EnemyAudio enemyAudio in enemyAudios)
        {
            if (!enemyAudioDictionary.ContainsKey(enemyAudio.enemyType))
            {
                enemyAudioDictionary.Add(enemyAudio.enemyType, enemyAudio.audioClip);
            }
        }
    }

    // Start background music when the game starts
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
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

    // Function to change the volume of the player source
    public void ChangePlayerVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);
        playerSource.volume = masterVolume;
    }

    // Function to change the volume of the SFX source
    public void ChangeSFXVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);
        SFXSource.volume = masterVolume;
    }

    // Function to change the volume of the music source
    public void ChangeMusicVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);
        musicSource.volume = masterVolume;
    }

    // Function to change the volume of the player movement source
    public void ChangePlayerMovementVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);
        playerMovementSource.volume = masterVolume;
    }

    // Function to change the volume of the player attack source
    public void ChangePlayerAttackVolume(float volume)
    {
        masterVolume = Mathf.Clamp(volume, 0f, 1f);
        playerAttackSource.volume = masterVolume;
    }
}
