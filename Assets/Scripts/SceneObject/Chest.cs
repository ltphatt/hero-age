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

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isOpened) return;

        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            switch (chestType)
            {
                case ChestType.Golden:
                    Debug.Log("You found a golden chest!");
                    break;
                case ChestType.Silver:
                    Debug.Log("You found a silver chest!");
                    break;
                case ChestType.Bronze:
                    Debug.Log("You found a bronze chest!");
                    break;
                case ChestType.Wooden:
                    Debug.Log("You found a wooden chest!");
                    break;
            }
            audioManager.PlaySFX(audioManager.chest, gameObject);
            animator.SetTrigger("Open");
            isOpened = true;
        }
    }
}
