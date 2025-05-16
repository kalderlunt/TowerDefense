using Managers;
using Entities.Player;
using TMPro;
using UnityEngine;

namespace HUD.MoneyInGame
{
    public class DisplayMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        private PlayerMoneyInGame playerMoney;

        private void Start()
        {
            playerMoney = PlayerMoneyInGame.instance;
            //EventManager.instance.onRefreshMoneyPlayerInGame.AddListener(RefreshText);
            EventManager.instance.onRefreshMoneyPlayerInGame += RefreshText;
            RefreshText();
        }

        public void RefreshText()
        {
            moneyText.text = $"${playerMoney.money}";
        }
    }
}