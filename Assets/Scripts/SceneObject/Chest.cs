using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChestType
{
    Golden,
    Silver,
    Bronze,
    Wooden,
}

public class Chest : MonoBehaviour
{
    [SerializeField] ChestType chestType;
    Animator animator;
    AudioManager audioManager;

    private bool isOpened = false;

    PlayerController playerController;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        var player = other.GetComponent<PlayerChat>();
        if (player != null)
        {
            switch (chestType)
            {
                case ChestType.Golden:
                    Debug.Log("You found a golden chest!");
                    playerController.ChangeCoin(10);
                    break;
                case ChestType.Silver:
                    Debug.Log("You found a silver chest!");
                    playerController.ChangeCoin(5);
                    break;
                case ChestType.Bronze:
                    Debug.Log("You found a bronze chest!");
                    playerController.ChangeCoin(3);
                    break;
                case ChestType.Wooden:
                    Debug.Log("You found a wooden chest!");
                    playerController.ChangeCoin(2);
                    break;
            }
            audioManager.PlaySFX(audioManager.chest, gameObject);
            animator.SetTrigger("Open");
            isOpened = true;

            player.DisplayOpenChestMessage(chestType.ToString());
        }
    }
}
