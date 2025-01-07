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
    }

    void GameOverScreen()
    {
        gamerOverScreen.SetActive(true);
        survivedText.text = "You survived " + survivedLevelsCount + " level";
        if (survivedLevelsCount > 1) survivedText.text += "s";

        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        currentLevel = 1;
        survivedLevelsCount = 0;

        Time.timeScale = 1;
        SceneManager.LoadScene(currentLevel - 1);
    }
}
