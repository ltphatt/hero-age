using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (BossHealth.isBossDefeated) // Kiểm tra Boss đã bị tiêu diệt
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
