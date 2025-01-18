using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionsController : MonoBehaviour
{
    public GameObject settingsScreen;
    public GameObject tutorialScreen;

    public void OnStartClick()
    {
        PlayerPrefs.DeleteKey("Checkpoint");
        PlayerPrefs.DeleteKey("MaxHP");
        PlayerPrefs.DeleteKey("MaxMP");
        PlayerPrefs.DeleteKey("Coin");
        PlayerPrefs.DeleteKey("HP");
        PlayerPrefs.DeleteKey("MP");
        PlayerPrefs.DeleteKey("Lives");

        SceneManager.LoadScene("Level 1");
    }

    public void OnContinueClick()
    {
        if (PlayerPrefs.HasKey("Checkpoint"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Checkpoint"));
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void OnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void OnSettingsClick()
    {
        settingsScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnTutorialClick()
    {
        tutorialScreen.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnBackClick()
    {
        settingsScreen.SetActive(false);
        tutorialScreen.SetActive(false);
        gameObject.SetActive(true);
    }
}
