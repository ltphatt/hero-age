using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    public int damagePerTick = 10;
    public float damageInterval = 1f;

    private bool isActivated = false;
    private float timer = 0f;
    private PlayerController player; // Tham chiếu tới PlayerController

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActivated = true;
            player = collision.GetComponent<PlayerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isActivated = false;
            player = null; // Xóa tham chiếu khi Player rời đi
        }
    }

    private void Update()
    {
        if (isActivated)
        {
            timer += Time.deltaTime;

            if (timer >= damageInterval)
            {
                timer = 0f;

                if (player != null)
                {
                    player.TakeDamage(damagePerTick); // Gây sát thương nếu Player tồn tại
                }
                else
                {
                    Debug.LogWarning("Player reference is null. Damage was not applied.");
                    isActivated = false; // Hủy kích hoạt nếu Player không còn tồn tại
                }
            }
        }
    }
}
