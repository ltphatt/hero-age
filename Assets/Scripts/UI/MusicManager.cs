using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    private AudioSource audioSource;
    public AudioClip backgroundMusic;

    [SerializeField] private Slider musicSlider;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            // audioSource.clip = backgroundMusic;
            // audioSource.Play();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        if (backgroundMusic != null)
        {
            PlayBackgroundMusic(false, backgroundMusic);
        }

        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicSlider.value); });
    }

    public static void SetVolume(float volume)
    {
        instance.audioSource.volume = volume;
    }

    public void PlayBackgroundMusic(bool wantReset, AudioClip audioClip = null)
    {
        if (audioClip != null) audioSource.clip = audioClip;
        if (audioSource.clip == null) return;
        if (wantReset) audioSource.Stop();

        audioSource.Play();
    }

    public void PauseBackgroundMusic()
    {
        audioSource.Pause();
    }
}
