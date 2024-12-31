using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    [SerializeField] EnemyController enemy;
    [SerializeField] BossHealth boss;
    [SerializeField] Image barImage;
    Canvas canvas;
    [SerializeField] Color green = Color.green;
    [SerializeField] Color yellow = Color.yellow;
    [SerializeField] Color red = Color.red;
    [SerializeField] bool isBoss = false;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        barImage.fillAmount = 1f;
        Hide();
    }

    private void Update()
    {
        if (isBoss)
        {
            HandleBossHealthChange();
        }
        else
        {
            HandleEnemyHealthChange();
        }
    }

    private void Show()
    {
        canvas.enabled = true;
    }

    private void Hide()
    {
        canvas.enabled = false;
    }

    void HandleEnemyHealthChange()
    {
        if (enemy != null)
        {
            barImage.fillAmount = (float)enemy.GetHP() / enemy.GetMaxHP();
            UpdateBarColor();

            if (enemy.GetState() == EnemyController.State.OutCombat)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    void HandleBossHealthChange()
    {
        if (boss != null)
        {
            if (boss.transform.localScale.z < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            barImage.fillAmount = (float)boss.health / boss.maxHealth;
            UpdateBarColor();

            if (boss.health <= 0)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }
    }

    void UpdateBarColor()
    {
        float fill = barImage.fillAmount;
        if (fill > 0.7f)
        {
            barImage.color = green;
        }
        else if (fill > 0.3)
        {
            barImage.color = yellow;
        }
        else
        {
            barImage.color = red;
        }
    }

}
