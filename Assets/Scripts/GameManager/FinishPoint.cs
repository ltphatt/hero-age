using System.Collections;
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
        var player = collision.GetComponent<PlayerChat>();
        if (player != null)
        {
            Debug.Log("Player đã đến Finish Point!");
            if (BossHealth.isBossDefeated)
            {
                audioManager.PlaySFX(audioManager.finishPoint, gameObject);
                SceneController.instance.NextLevel();
            }
            else
            {
                player.DisplayCannotSwitchScene();
            }
        }
    }
}
