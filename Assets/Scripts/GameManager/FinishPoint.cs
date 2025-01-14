using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player đã đến Finish Point!");
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
