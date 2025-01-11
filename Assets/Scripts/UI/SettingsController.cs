using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject OriginScreen;
    private VolumeSettings volumeSetting;

    private void Start()
    {
        volumeSetting = FindObjectOfType<VolumeSettings>();
    }

    public void OnSaveClick()
    {
        volumeSetting.SaveSettings();
        OriginScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnCancelClick()
    {
        volumeSetting.LoadSettings();
        OriginScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
