using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{

    public int healValue = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HealPlayer(PlayerController player)
    {
        player.ChangeHealth(healValue);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
