using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Towers.Patrol
{
    [RequireComponent(typeof(PatrolMovement))]
    public class Patrol : MonoBehaviour//, IPooledObject<Patrol>
    {
        private PatrolData data;
        //private Pool<Patrol> pool;
        private TowerRange colliderCar;
        //public event Action onDestroyPatrol;

        
        private void Start()
        {
            colliderCar = GetComponentInChildren<TowerRange>();
            Assert.IsNotNull(colliderCar, $"Tower {name} n'a pas de TowerRange");
            
            if (colliderCar)
                colliderCar.EventEnterInRange += OnEnemyEnterRange;
        }

        private void OnEnemyEnterRange(Collider collision)
        {
            if (IsEnemy(collision.gameObject) /*&& IsInRange(collision.transform)*/)
            {
                Attack(collision.gameObject);
            }
        }
        
        private bool IsEnemy(GameObject obj)
        {
            return obj.GetComponent<Enemy>();
        }
        
        private void Attack(GameObject enemyTarget)
        {
            //Debug.Log($"{gameObject} Shouting");
            Enemy enemy = enemyTarget.GetComponent<Enemy>();
            Assert.IsNotNull(enemy, $"L'objet {enemyTarget.name} n'a pas de script Enemy attach�.");

            float enemyHealth = enemy.data.health;
            enemy.TakeDamage(data.health);
            TakeDamage(enemyHealth);
        
            Debug.Log($"enemy.data.health : {enemy.data.health}");
            Debug.Log($"patrol.data.health : {data.health}");
            //Play Sound
            //AudioManager.instance.PlaySfx(data.attackSound);
        }
        
        
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

        public void DestroyPatrol()
        {
            Debug.Log("Patrol Destroyed");
            Destroy(gameObject);
            
            //PlaySfx Destruction sound
            
            /*onDestroyPatrol?.Invoke();
            onDestroyPatrol = null;*/
        }
    }
}