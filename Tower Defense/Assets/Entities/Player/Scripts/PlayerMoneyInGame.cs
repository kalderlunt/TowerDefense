using Managers;
using UnityEngine;

namespace Entities.Player
{
    public class PlayerMoneyInGame : MonoBehaviour
    {
        public static PlayerMoneyInGame instance;
    
        [SerializeField] private int moneyStart = 400;
        public int money { get; set; }


        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Debug.Log("Money : " + money); 
            money = moneyStart;
            Debug.Log("Money : " + money);
        }

        private void Start()
        {
            // UnityEvent - EventManager.instance.AddMoneyPlayerInGame.AddListener(AddMoney);
            EventManager.instance.AddMoneyPlayerInGame += AddMoney;
        }
        
        public void AddMoney(int amount)
        {
            money += amount;
            EventManager.instance.onRefreshMoneyPlayerInGame.Invoke();
            //Debug.Log("Money : " + money);
        }
    }
}