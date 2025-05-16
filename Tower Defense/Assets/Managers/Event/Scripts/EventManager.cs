using System;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    [DefaultExecutionOrder(-100)]
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance { get; private set; }

        //public Action onCancelPlaceTower { get; set; }
        public Action onCancelPlaceTower;
        
        public Action onRefreshMoneyPlayerInGame;
        
        public Action<int> AddMoneyPlayerInGame;
        
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                return;
            }

            Destroy(gameObject);
        }
    }
}