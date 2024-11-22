using UnityEngine;

[RequireComponent(typeof(EnemyData))]
public class Enemy : MonoBehaviour
{
    EnemyData data;

    private void Awake()
    {
        data = GetComponent<EnemyData>();
    }

    public void TakeDamage(float damage, TowerData.DamageType damageType)
    {
        data.health -= damage;

        if (data.health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log($"{name} a été détruit !");
        Destroy(gameObject);
    }
}