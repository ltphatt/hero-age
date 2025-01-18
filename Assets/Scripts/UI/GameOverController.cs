using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gamerOverScreen;
    public TMP_Text survivedText;
    public int currentLevel = 1;
    public int survivedLevelsCount;

    void OnDisable()
    {
        PlayerController.OnPlayerDied -= ShowGameOverScreen;
    }

    void Start()
    {
        PlayerController.OnPlayerDied += ShowGameOverScreen;

        survivedLevelsCount = currentLevel - 1;
        HideGameOverScreen();
    }

    public void HideGameOverScreen()
    {
        Time.timeScale = 1f;
        gamerOverScreen.SetActive(false);
    }

    public void ShowGameOverScreen()
    {
        gamerOverScreen.SetActive(true);
        survivedText.text = "You survived " + survivedLevelsCount + " level";
        if (survivedLevelsCount > 1) survivedText.text += "s";

        Time.timeScale = 0f;
    }

    public void ResetGame()
    {
        currentLevel = 1;
        survivedLevelsCount = 0;

        HideGameOverScreen();
        SceneManager.LoadScene("Level " + currentLevel);
    }

    public void ExitGamePlaying()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
