using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public Slider musicSlider;
    [SerializeField] public Slider sfxSlider;

    private void Start()
    {
        LoadSettings();

        musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSFXVolume(); });
    }

    public void LoadSettings()
    {
        LoadMusicVolume();
        LoadSFXVolume();
    }

    public void SaveSettings()
    {
        SaveMusicVolume();
        SaveSFXVolume();
    }

    public void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void SaveSFXVolume()
    {
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;

        audioMixer.SetFloat("music", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;

        audioMixer.SetFloat("sfx", volume);
        audioMixer.SetFloat("player", volume);
        audioMixer.SetFloat("enemy", volume);
        audioMixer.SetFloat("playerMovement", volume);
        audioMixer.SetFloat("playerSkill", volume);
        audioMixer.SetFloat("playerAttack", volume);
        audioMixer.SetFloat("bossAttack", volume);

    }

    public void LoadMusicVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
            SetMusicVolume();
        }
        else
        {
            musicSlider.value = musicSlider.maxValue;
            SetMusicVolume();
        }
    }

    public void LoadSFXVolume()
    {
        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            SetSFXVolume();
        }
        else
        {
            sfxSlider.value = sfxSlider.maxValue;
            SetSFXVolume();
        }
    }
}
