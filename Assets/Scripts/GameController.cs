using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
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
        // reset time scale to normal
        Time.timeScale = 1f;

        PlayerController.OnPlayerDied += ShowGameOverScreen;

        survivedLevelsCount = currentLevel - 1;
        HideGameOverScreen();
    }

    public void HideGameOverScreen()
    {
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

        Time.timeScale = 1f;
        SceneManager.LoadScene("Level " + currentLevel);
    }

    public void ExitGamePlaying()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start Scene");
    }
}
