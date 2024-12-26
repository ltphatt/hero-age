using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{

    public int healValue = 1;
    [SerializeField] GameObject itemFeedbackPrefab;
    [Header("Item Sound")]
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void HealPlayer(PlayerController player)
    {
        player.ChangeHealth(healValue);
    }

    public void DestroySelf()
    {
        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject, 1f);
    }
    public void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

}
