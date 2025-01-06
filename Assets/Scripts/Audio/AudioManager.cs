using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----------------Audio Sources----------------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------------Audio Clips----------------")]
    public AudioClip background;
    public AudioClip death;
    public AudioClip checkpoint;
    public AudioClip wallTouch;
    public AudioClip playerArchery;
    public AudioClip jerry;
    public AudioClip gem;
    public AudioClip amulet;
    public AudioClip bigGem;


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
}
