using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
  public static SceneController instance;
  [SerializeField] Animator transitionAnim;

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

  public void NextLevel()
  {
    StartCoroutine(LoadScence());
  }

  IEnumerator LoadScence()
  {

    transitionAnim.SetTrigger("End");
    yield return new WaitForSeconds(1);
    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    transitionAnim.SetTrigger("Start");
  }

  public void LoadScence(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }

  public void LoadStartScene()
  {
    SceneManager.LoadScene("Start Scene");
  }
}
