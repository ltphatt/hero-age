using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{

    public int healValue = 1;
    [SerializeField] GameObject itemFeedbackPrefab;


    public void HealPlayer(PlayerController player)
    {
        player.ChangeHealth(healValue);
    }

    public void DestroySelf()
    {
        Instantiate(itemFeedbackPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
