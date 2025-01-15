using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{

    public int healValue = 1;
    [SerializeField] GameObject itemFeedbackPrefab;
    [Header("Audio Manager")]
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void HealPlayer(PlayerController player)
    {
        // Play SFX when collecting health
        audioManager.PlaySFX(audioManager.jerry, gameObject);

        player.ChangeHealth(healValue);
    }

    public void DestroySelf()
    {
        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
