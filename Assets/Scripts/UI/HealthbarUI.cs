using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    EnemyController enemy;
    [SerializeField] Image barImage;

    private void Awake()
    {
        enemy = FindObjectOfType<EnemyController>();
    }

    private void Start()
    {
        barImage.fillAmount = 1f;
    }

    private void Update()
    {
        if (enemy != null)
        {
            barImage.fillAmount = (float)enemy.GetHP() / enemy.GetMaxHP();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
