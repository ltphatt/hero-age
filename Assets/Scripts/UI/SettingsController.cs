using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject StartScreen;
    private VolumeSettings volumeSetting;

    private void Start()
    {
        volumeSetting = FindObjectOfType<VolumeSettings>();
    }

    public void OnSaveClick()
    {
        volumeSetting.SaveSettings();
        StartScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnCancelClick()
    {
        volumeSetting.LoadSettings();
        StartScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
