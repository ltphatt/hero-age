using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (BossHealth.isBossDefeated)
            {
                SceneController.instance.NextLevel();
            }
            else
            {
                Debug.Log("Boss chưa bị tiêu diệt! Không thể qua Level.");
            }
        }
    }
}
