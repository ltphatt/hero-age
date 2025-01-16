using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerChat : MonoBehaviour
{
    [SerializeField] GameObject chatPanel;
    [SerializeField] TextMeshProUGUI chatTxt;
    [SerializeField] float displayTime = 3f;

    private void Start()
    {
        chatPanel.SetActive(false);
    }

    public void DisplayOpenChestMessage(string chestType)
    {
        chatTxt.text = $"You found a {chestType} chest!";
        chatPanel.SetActive(true);

        StartCoroutine(HideChat());
    }

    public void DisplayCannotSwitchScene()
    {
        chatTxt.text = "Hãy tiêu diệt Boss để qua màn !!";
        chatPanel.SetActive(true);

        StartCoroutine(HideChat());
    }

    public void DisplayDefeatingBossMessage()
    {
        chatTxt.text = "Đã tiêu diệt Boss !!";
        chatPanel.SetActive(true);

        StartCoroutine(HideChat());
    }

    public void DisplayCollectingBigGemMessage()
    {
        chatTxt.text = "Đã tìm lại 1 viên pha lê !!";
        chatPanel.SetActive(true);

        StartCoroutine(HideChat());
    }

    private IEnumerator HideChat()
    {
        yield return new WaitForSeconds(displayTime);
        chatPanel.SetActive(false);
    }
}
