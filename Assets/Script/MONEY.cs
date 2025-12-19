using TMPro;
using UnityEngine;

public class PlayerMoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public string moneyKey = "coins";

    void Start()
    {
        UpdateMoneyUI();
        InvokeRepeating(nameof(UpdateMoneyUI), 0f, 1f);
    }

    void UpdateMoneyUI()
    {
        int money = PlayerPrefs.GetInt(moneyKey, 0);
        moneyText.text = money.ToString();
    }
}
