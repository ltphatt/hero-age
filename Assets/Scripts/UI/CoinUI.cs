using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Image coinPrefab;
    public TMP_Text coinCountPrefab;

    private TMP_Text coinCount;

    void Start()
    {
        Instantiate(coinPrefab, transform);
        coinCount = Instantiate(coinCountPrefab, transform);

        PlayerController.OnPlayerCoinChange += UpdateCoins;
    }

    void OnDestroy()
    {
        PlayerController.OnPlayerCoinChange -= UpdateCoins;
    }

    public void UpdateCoins(int coin)
    {
        coinCount.text = coin.ToString();
    }
}
