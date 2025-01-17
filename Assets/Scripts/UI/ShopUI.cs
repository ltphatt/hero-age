using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject OpenShopButton;
    public GameObject ShopBoard;

    void Start()
    {
        Hide();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (ShopBoard.activeSelf)
            {
                Hide();
            }
            else
            {
                Show();
                Debug.Log("ShopUI is active");
            }
        }
    }

    void Hide()
    {
        OpenShopButton.SetActive(true);
        ShopBoard.SetActive(false);
        Time.timeScale = 1;
    }

    void Show()
    {
        OpenShopButton.SetActive(false);
        ShopBoard.SetActive(true);
        Time.timeScale = 0;
    }

    public void OnOpenShopButtonClicked()
    {
        Show();
    }

    public void OnCloseShopButtonClicked()
    {
        Hide();
    }
}
