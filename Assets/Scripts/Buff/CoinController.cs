using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int coinValue = 1;
    [SerializeField] GameObject itemFeedbackPrefab;
    [Header("Audio Manager")]
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void CollectCoin(PlayerController player)
    {
        // Play SFX when collecting coin
        audioManager.PlaySFX(audioManager.gem, gameObject);
        player.ChangeCoin(coinValue);
    }

    public void DestroySelf()
    {

        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
