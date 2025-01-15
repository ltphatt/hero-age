using System.Collections;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerChat>();
        if (player != null)
        {
            if (BossHealth.isBossDefeated)
            {
                SceneController.instance.NextLevel();
            }
            else
            {
                player.DisplayCannotSwitchScene();
            }
        }
    }
}
