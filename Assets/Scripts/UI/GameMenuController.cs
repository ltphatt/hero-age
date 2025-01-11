using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public GameObject gameMenuScreen;
    public GameObject gameMenuButton;
    public GameObject settingsScreen;

    void Start()
    {
        HideGameMenu();
        ShowGameMenuButton();
    }

    public void FreezeBackground()
    {
        Time.timeScale = 0f;
    }

    public void UnfreezeBackground()
    {
        Time.timeScale = 1f;
    }

    public void ShowGameMenu()
    {
        gameMenuScreen.SetActive(true);
    }

    public void HideGameMenu()
    {
        gameMenuScreen.SetActive(false);
    }

    public void HideGameMenuButton()
    {
        gameMenuButton.SetActive(false);
    }

    public void ShowGameMenuButton()
    {
        gameMenuButton.SetActive(true);
    }

    public void HideSettingsScreen()
    {
        settingsScreen.SetActive(false);
    }

    public void ShowSettingsScreen()
    {
        settingsScreen.SetActive(true);
    }
    public void OnGameMenuButtonClick()
    {
        if (gameMenuScreen.activeSelf)
        {
            HideGameMenu();
            ShowGameMenuButton();
        }
        else
        {
            ShowGameMenu();
            HideGameMenuButton();
        }
    }
    public void OnCloseButtonClick()
    {
        HideGameMenu();
        ShowGameMenuButton();
    }
}
