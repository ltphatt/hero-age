using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int coinValue = 1;
    [SerializeField] GameObject itemFeedbackPrefab;
    [Header("Item Sound")]
    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void CollectCoin(PlayerController player)
    {
        player.ChangeCoin(coinValue);
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
