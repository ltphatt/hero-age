using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    private Vector3 currentCheckpoint;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        currentCheckpoint = transform.position;
        Debug.Log("Default checkpoint set to player start position: " + currentCheckpoint);
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerRespawn += HandleRespawn;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerRespawn -= HandleRespawn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Checkpoint"))
        {
            currentCheckpoint = collision.transform.position;
            Debug.Log("Checkpoint updated: " + currentCheckpoint);
        }
    }

    private void HandleRespawn(int remainingLives)
    {
        transform.position = currentCheckpoint;

        playerController.ResetStats();

        Debug.Log($"Player respawned at checkpoint: {currentCheckpoint}, Lives left: {remainingLives}");
    }
}
