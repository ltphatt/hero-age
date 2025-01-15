using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player đã đến Finish Point!");
            if (BossHealth.isBossDefeated)
            {
                audioManager.PlaySFX(audioManager.checkpoint, gameObject);
                SceneController.instance.NextLevel();
            }
            else
            {
                Debug.Log("Boss chưa bị tiêu diệt! Không thể qua Level.");
            }
        }
    }
}
