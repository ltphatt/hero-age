using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gamerOverScreen;
    public TMP_Text survivedText;
    public int currentLevel = 1;
    public int survivedLevelsCount;

    void Start()
    {
        PlayerController.OnPlayerDied += GameOverScreen;

        survivedLevelsCount = currentLevel - 1;
        gamerOverScreen.SetActive(false);

        Debug.Log("Reset game over screen");
    }

    void GameOverScreen()
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
        SceneManager.LoadScene(currentLevel - 1);
    }
}
