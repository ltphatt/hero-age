using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletController : MonoBehaviour
{
    [SerializeField] private float buffDuration = 5f;
    [SerializeField] private int buffMultiplier = 2;
    [SerializeField] GameObject itemFeedbackPrefab;
    [Header("Item Sound")]
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void CollectBuff(PlayerController player)
    {
        player.ApplyAmuletBuff(buffDuration, buffMultiplier);
    }

    public void DestroySelf()
    {

        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject, 0.7f);
    }
    public void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}
