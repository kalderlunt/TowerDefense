using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Towers.Patrol
{
    [RequireComponent(typeof(PatrolData))]
    public class PatrolBehaviour : MonoBehaviour, IPooledObject<PatrolBehaviour>
    {
        private PatrolData data;
        private Pool<PatrolBehaviour> pool;

        public event Action onDeath;
        
        public void TakeDamage(float damage)
        {
            data.health -= damage;

            if (data.health <= 0)
            {
                DestroyPatrol();
            }
        }
        
        public void SetPool(Pool<PatrolBehaviour> pool)
        {
            this.pool = pool;
        }

        public void ResetData()
        {
            data.ResetData();
        }

        private void DestroyPatrol()
        {
            onDeath?.Invoke();
            onDeath = null;
        }
    }
}