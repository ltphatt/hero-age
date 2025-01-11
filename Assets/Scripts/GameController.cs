using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gamerOverScreen;
    public TMP_Text survivedText;
    public int currentLevel = 1;
    public int survivedLevelsCount;

    public static GameController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDisable()
    {
        PlayerController.OnPlayerDied -= GameOverScreen;
    }

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

        Time.timeScale = 0f;
    }

    public void ResetGame()
    {
        currentLevel = 1;
        survivedLevelsCount = 0;

        Time.timeScale = 1f;
        SceneManager.LoadScene(currentLevel - 1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
