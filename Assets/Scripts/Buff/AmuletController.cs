using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletController : MonoBehaviour
{
    [SerializeField] private float buffDuration = 5f;
    [SerializeField] private int buffMultiplier = 2;
    [SerializeField] GameObject itemFeedbackPrefab;
    [Header("Audio Manager")]
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void CollectBuff(PlayerController player)
    {
        // Play SFX when collecting amulet
        audioManager.PlaySFX(audioManager.amulet);

        player.ApplyAmuletBuff(buffDuration, buffMultiplier);
    }

    public void DestroySelf()
    {

        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject, 0.7f);
    }
}
