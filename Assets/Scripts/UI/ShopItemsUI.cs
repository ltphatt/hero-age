using System;
using UnityEngine;

public class ShopItemsUI : MonoBehaviour
{
    public int addMaxHPAmount = 2;
    public int addMaxHPPrice = 5;
    public int addMaxMPAmount = 2;
    public int addMaxMPPrice = 5;

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
}
