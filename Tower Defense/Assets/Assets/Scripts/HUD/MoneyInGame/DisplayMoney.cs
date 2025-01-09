using Assets.Scripts.Managers;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.HUD.MoneyInGame
{
    public class DisplayMoney : MonoBehaviour
    {
        [SerializeField] private TMP_Text moneyText;
        private PlayerMoneyInGame playerMoney;

        private void Start()
        {
            playerMoney = PlayerMoneyInGame.instance;
            EventManager.instance.onRefreshMoneyPlayerInGame.AddListener(RefreshText);
            RefreshText();
        }

        public void RefreshText()
        {
            moneyText.text = $"${playerMoney.money}";
        }
    }
}