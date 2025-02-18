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
        UnfreezeBackground();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsScreen.activeSelf)
            {
                HideSettingsScreen();
                ShowGameMenu();
            }
            else
            {
                OnGameMenuButtonClick();
            }
        }
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
        FreezeBackground();
    }

    public void HideGameMenu()
    {
        gameMenuScreen.SetActive(false);
        UnfreezeBackground();
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
        FreezeBackground();
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

    public void OnHomeButtonClick()
    {
        HideGameMenu();
        ShowGameMenuButton();

        // This code is not available in the current context
        // SceneController.instance.LoadStartScene();

        SceneManager.LoadScene("Start Scene");
    }

    public void OnContinueButtonClick()
    {
        HideGameMenu();
        ShowGameMenuButton();
    }

    public void OnSettingsButtonClick()
    {
        HideGameMenu();
        ShowSettingsScreen();
    }
}
