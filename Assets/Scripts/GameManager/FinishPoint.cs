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

                PlayerController player = collision.GetComponent<PlayerController>();

                if (SceneManager.GetActiveScene().name == "Level 3")
                {
                    PlayerPrefs.DeleteKey("Checkpoint");
                    player.ResetPlayerData();
                }
                else
                {
                    PlayerPrefs.SetInt("Checkpoint", SceneManager.GetActiveScene().buildIndex + 1);
                    player.SavePlayerData();
                }
            }
            else
            {
                playerChat.DisplayCannotSwitchScene();
            }
        }
    }
}
