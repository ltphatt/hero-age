using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    [SerializeField] private GameObject crankUp;
    [SerializeField] private GameObject crankDown;
    [SerializeField] private GameObject gate;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
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
            audioManager.PlaySFX(audioManager.crank, gameObject);
            crankUp.SetActive(false);
            crankDown.SetActive(true);
            gate.SetActive(false);
        }
    }
}
