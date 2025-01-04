using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] private GameObject crankUp;
    [SerializeField] private GameObject crankDown;

    private void Start()
    {
        crankUp.SetActive(true);
        crankDown.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            crankUp.SetActive(false);
            crankDown.SetActive(true);
        }
    }
}
