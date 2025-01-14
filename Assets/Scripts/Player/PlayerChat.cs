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

    private IEnumerator HideChat()
    {
        yield return new WaitForSeconds(displayTime);
        chatPanel.SetActive(false);
    }
}
