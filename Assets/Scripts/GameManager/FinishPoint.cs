using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerChat>(out var playerChat))
        {
            if (BossHealth.isBossDefeated)
            {
                audioManager.PlaySFX(audioManager.finishPoint, gameObject);
                SceneController.instance.NextLevel();

                if (SceneManager.GetActiveScene().name == "Level 3")
                {
                    PlayerPrefs.DeleteKey("Checkpoint");
                }
                else
                {
                    PlayerPrefs.SetInt("Checkpoint", SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
                playerChat.DisplayCannotSwitchScene();
            }
        }
    }
}
