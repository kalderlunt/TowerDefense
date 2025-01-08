using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Towers.Patrol
{
    [RequireComponent(typeof(PatrolMovement))]
    public class Patrol : MonoBehaviour//, IPooledObject<Patrol>
    {
        private PatrolData data;
        //private Pool<Patrol> pool;
        public event Action onDestroyPatrol;

        public void TakeDamage(float damage)
        {
            data.health -= damage;

            if (data.health <= 0)
            {
                DestroyPatrol();
            }
        }
        
        /*public void SetPool(Pool<Patrol> pool)
        {
            this.pool = pool;
        }*/

        public void ResetData(PatrolData data)
        {
            data.ResetHealth();
        }

        public PatrolData GetData()
        {
            return data;
        }

        public void SetData(PatrolData data)
        {
            this.data = data;
        }

        private void DestroyPatrol()
        {
            onDestroyPatrol?.Invoke();
            onDestroyPatrol = null;
        }
    }
}