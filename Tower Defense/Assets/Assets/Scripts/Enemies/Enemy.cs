using System;
using Assets.Scripts.Managers;
using UnityEngine;

[RequireComponent(typeof(EnemyData))]
public class Enemy : MonoBehaviour, IPooledObject<Enemy>
{
    public EnemyData data { get; private set; }

    private Pool<Enemy> pool;
    public event Action onDeath;

    private void Awake()
    {
        data = GetComponent<EnemyData>();
    }

    public void TakeDamage(float damage)
    {
        float lastHealthAmmount = data.health;
        data.health -= damage;
            
        float healthClamp = Mathf.Clamp(data.health, 0, data.maxHealth);
        float resultToAddMoney = lastHealthAmmount - healthClamp;
            
        EventManager.instance.AddMoneyPlayerInGame?.Invoke((int)resultToAddMoney * 4);

        if (data.health <= 0)
        {
            Die();
        }
    }

    public void SetPool(Pool<Enemy> pool)
    {
        this.pool = pool;
    }

    public void ResetData()
    {
        data.ResetData();
    }

    private void Die()
    {
        //Debug.Log($"{name} a �t� d�truit !");
        //Destroy(gameObject);
        onDeath?.Invoke();
        onDeath = null;    // Nettoyage des abonnements
    }
}