using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("Audio Player Sources")]
    [SerializeField] AudioSource playerMovementSource;
    [SerializeField] AudioSource playerAttackSource;
    [SerializeField] AudioSource playerSource;


    [Header("Audio Clips Background Music")]
    public AudioClip background;
    [Header("Audio Player SFX")]
    public AudioClip death;
    public AudioClip jump;
    public AudioClip movement;
    public AudioClip attack;
    public AudioClip hit;
    public AudioClip wallTouch;
    public AudioClip playerArchery;
    [Header("Audio Item SFX")]
    public AudioClip checkpoint;
    public AudioClip jerry;
    public AudioClip gem;
    public AudioClip amulet;
    public AudioClip bigGem;
    [Header("Audio Enemy Hit")]
    [SerializeField] public List<EnemyAudio> enemyAudios;
    private Dictionary<string, AudioClip> enemyAudioDictionary;

    // Awake function to initialize the dictionary
    private void Awake()
    {
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
            if (!SFXSource.isPlaying)
            {
                SFXSource.PlayOneShot(audioClip);
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
        // if (!playerMovementSource.isPlaying)
        // {

        // }
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
