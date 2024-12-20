using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int coinValue = 1;
    [SerializeField] GameObject itemFeedbackPrefab;

    public void CollectCoin(PlayerController player)
    {
        player.ChangeCoin(coinValue);
    }

    public void DestroySelf()
    {
        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
