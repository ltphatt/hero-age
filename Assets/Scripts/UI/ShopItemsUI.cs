using System;
using UnityEngine;

public class ShopItemsUI : MonoBehaviour
{
    [Header("Add Max HP")]
    public int addMaxHPAmount = 2;
    public int addMaxHPPrice = 5;

    [Header("Add Max MP")]
    public int addMaxMPAmount = 2;
    public int addMaxMPPrice = 5;

    [Header("Regain 50% HP")]
    public int regain50HPPrice = 5;

    [Header("Regain 100% HP")]
    public int regain100HPPrice = 10;

    PlayerController playerController;

    void OnEnable()
    {
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = 1;
        playerController.SavePlayerData();
    }

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void DisplayNotEnoughCoinsMessage()
    {
        var chat = FindObjectOfType<PlayerChat>();

        chat.DisplayNotEnoughCoinsMessage();
    }

    private void BuyItem(int price, Action method)
    {
        if (playerController.Coin < price)
        {
            DisplayNotEnoughCoinsMessage();
        }
        else
        {
            method.DynamicInvoke();
            playerController.ChangeCoin(-price);
        }
    }

    public void OnAddMaxHPClick()
    {
        BuyItem(addMaxHPPrice, () => playerController.ChangeMaxHP(addMaxHPAmount));
    }

    public void OnAddMaxMPClick()
    {
        BuyItem(addMaxMPPrice, () => playerController.ChangeMaxMP(addMaxMPAmount));
    }

    public void OnRegain50HPClick()
    {
        BuyItem(regain50HPPrice, () => playerController.ChangeHealth(playerController.maxHP / 2));
    }

    public void OnRegain100HPClick()
    {
        BuyItem(regain100HPPrice, () => playerController.ChangeHealth(playerController.maxHP));
    }
}
