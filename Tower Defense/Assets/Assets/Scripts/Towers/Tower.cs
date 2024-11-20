using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerData data; 

    [SerializeField] private Transform attackPoint; // Point de tir ou de ciblage
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private SphereCollider rangeCollider;

    private TowerRange towerRange;
    private float fireCooldown = 0f;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    private void OnEnable()
    {
        towerRange = GetComponentInChildren<TowerRange>();
        Assert.IsNotNull(towerRange, $"Tower {data.TowerName} n'a pas de TowerRange");

        towerRange.EventEnterInRange += OnEnemyEnterRange;
        towerRange.EventExitInRange += OnEnemyExitRange;
        
        rangeCollider.radius = data.baseRange * 0.5f;
        enemiesInRange.Clear();
        fireCooldown = 0f;
    }

    private void Update()
    {
        fireCooldown -= Time.deltaTime;

        // Supprimer les ennemis hors de portée
        enemiesInRange.RemoveAll(enemy => enemy == null || !IsInRange(enemy.transform));

        if (fireCooldown <= 0f && enemiesInRange.Count > 0)
        {
            switch (data.Mode)
            {
                case TowerData.AttackMode.Single:
                    Attack(enemiesInRange[0]); // Première cible
                    break;

                case TowerData.AttackMode.Multiple:
                    AttackMultiple(3); // Par exemple, attaque jusqu'à 3 cibles
                    break;

                case TowerData.AttackMode.Random:
                    Attack(enemiesInRange[Random.Range(0, enemiesInRange.Count)]);
                    break;

                case TowerData.AttackMode.Closest:
                    Attack(GetClosestEnemy());
                    break;

                case TowerData.AttackMode.Farthest:
                    Attack(GetFarthestEnemy());
                    break;
            }

            fireCooldown = 1f / data.baseFirerate; // Réinitialiser le cooldown
        }
    }

    private void OnEnemyEnterRange(Collider collision)
    {
        if (IsEnemy(collision.gameObject) /*&& IsInRange(collision.transform)*/)
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }

    private void OnEnemyExitRange(Collider collision)
    {
        if (IsEnemy(collision.gameObject))
        {
            enemiesInRange.Remove(collision.gameObject);
        }
    }

    private bool IsEnemy(GameObject obj)
    {
        Debug.Log($"{obj} est rentrer dans la zone pour etre check");
        return obj.GetComponent<Enemy>();
        //return ((1 << obj.layer) & enemyLayer) != 0;
    }

    private bool IsInRange(Transform target)
    {
        return Vector2.Distance(transform.position, target.position) <= data.baseRange * 5;
    }

    private GameObject GetClosestEnemy()
    {
        return enemiesInRange.OrderBy(e => Vector2.Distance(transform.position, e.transform.position)).FirstOrDefault();
    }

    private GameObject GetFarthestEnemy()
    {
        return enemiesInRange.OrderByDescending(e => Vector2.Distance(transform.position, e.transform.position)).FirstOrDefault();
    }

    private void AttackMultiple(int count)
    {
        var targets = enemiesInRange.Take(count); // Sélectionner les X premières cibles
        foreach (var target in targets)
        {
            Attack(target);
        }
    }

    private void Attack(GameObject enemy)
    {
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            float damage = data.baseDamage;

            transform.LookAt(new Vector3(enemy.transform.position.x, transform.position.y, enemy.transform.position.z));
            enemyScript.TakeDamage(damage, data.typeDamage);

            Debug.Log($"La tour {data.TowerName} a infligé {damage} dégâts à {enemy.name}.");

            // Play Sound
        }
    }
}