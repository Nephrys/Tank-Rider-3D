using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public string moneyKey = "Coins";

    void Start()
    {
        UpdateMoneyUI();
    }

    public void UpdateMoneyUI()
    {
        int money = PlayerPrefs.GetInt(moneyKey, 0);
        moneyText.text = money.ToString();
    }
}
