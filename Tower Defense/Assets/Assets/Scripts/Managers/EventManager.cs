using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Managers
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager instance { get; private set; }
        
        public UnityEvent onSpawnPatrol { get; private set; } = new();
        
        
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