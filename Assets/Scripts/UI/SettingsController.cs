using UnityEngine;

public class SettingsController : MonoBehaviour
{
    public GameObject StartScreen;


    public void OnSaveClick()
    {
        Debug.Log("Save settings");
    }

    public void OnCancelClick()
    {
        StartScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
