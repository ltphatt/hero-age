using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionsController : MonoBehaviour
{
    public GameObject settingsScreen;

    public void OnStartClick()
    {
        SceneManager.LoadScene("Level 1");
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
}
