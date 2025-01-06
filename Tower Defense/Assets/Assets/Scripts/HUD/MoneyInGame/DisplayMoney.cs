using TMPro;
using UnityEngine;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    private PlayerMoneyInGame playerMoney;

    private void Start()
    {
        playerMoney = PlayerMoneyInGame.instance;
        RefreshText();
    }

    public void RefreshText()
    {
        moneyText.text = $"${playerMoney.money}";
    }
}