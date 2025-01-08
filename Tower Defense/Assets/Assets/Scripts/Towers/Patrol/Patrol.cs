using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Towers.Patrol
{
    [RequireComponent(typeof(PatrolMovement))]
    public class Patrol : MonoBehaviour, IPooledObject<Patrol>
    {
        private PatrolData data;
        private Pool<Patrol> pool;
        public event Action onDeath;

        public void TakeDamage(float damage)
        {
            data.health -= damage;

            if (data.health <= 0)
            {
                DestroyPatrol();
            }
        }
        
        public void SetPool(Pool<Patrol> pool)
        {
            this.pool = pool;
        }

        public void ResetData(PatrolData data)
        {
            data.ResetData();
        }

        public PatrolData GetData()
        {
            return data;
        }

        private void DestroyPatrol()
        {
            onDeath?.Invoke();
            onDeath = null;
        }
    }
}