using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public int coinValue = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectCoin(PlayerController player)
    {
        player.ChangeCoin(coinValue);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
