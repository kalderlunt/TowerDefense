using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Data;
using Assets.Scripts.Managers;
using Assets.Scripts.Spawners;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent(typeof(TowerSelectable))]
public class Tower : MonoBehaviour
{
    public TowerData data;

    [SerializeField] private SphereCollider rangeCollider;

    private TowerRange towerRange;
    private float fireCooldown = 0f;
    private int burstCount = 0;
    private List<GameObject> enemiesInRange = new List<GameObject>();

    public DetectionZoneDisplay zoneDisplay { get; private set; }

    private TowerSelectable towerSelectable;


    private void Awake()
    {
        towerRange = GetComponentInChildren<TowerRange>();
        Assert.IsNotNull(towerRange, $"Tower {data.towerName} n'a pas de TowerRange");
        
        zoneDisplay = GetComponentInChildren<DetectionZoneDisplay>();
        Assert.IsNotNull(zoneDisplay, $"Tower {data.towerName} n'a pas de DetectionZoneDisplay");
        
        towerSelectable = GetComponent<TowerSelectable>();
        Assert.IsNotNull(towerSelectable, $"Tower {data.towerName} n'a pas de TowerSelectable");
    }

    private void OnEnable()
    {
        towerRange.EventEnterInRange += OnEnemyEnterRange;
        towerRange.EventExitInRange += OnEnemyExitRange;

        rangeCollider.radius = data.baseRange * 0.5f;
        enemiesInRange.Clear();
        fireCooldown = 0f;
        burstCount = 0;
    }

    private void OnDisable()
    {
        towerRange.EventEnterInRange -= OnEnemyEnterRange;
        towerRange.EventExitInRange -= OnEnemyExitRange;
    }

    private void Update()
    {
        if (!towerSelectable.isPlaced)
        {
            UpdatePreview();
            return;
        }

        UpdatePlaced();
    }

    private void UpdatePreview()
    {
        if (IsValidPlacement(transform.position))
        {
            zoneDisplay.ChangeColor(Color.green);
        }
        else
        {
            zoneDisplay.ChangeColor(Color.yellow);
        }
    }

    private bool IsValidPlacement(Vector3 position)
    {
        //pas d'objet bloquant, S'il est bien positionner en hauteur ou au sol.
        return true;
    }

    private void UpdatePlaced()
    {
        fireCooldown -= Time.deltaTime;

        // Supprimer les ennemis hors de port�e
        enemiesInRange.RemoveAll(enemy => enemy == null || !IsInRange(enemy.transform) || !enemy.activeInHierarchy);

        AttackByType();
    }

    private void AttackByType()
    {
        if (fireCooldown <= 0f && enemiesInRange.Count > 0)
        {
            switch (data.damageType)
            {
                case DamageType.Single:
                    SingleAttack(enemiesInRange[0]); // Premi�re cible
                    break;

                case DamageType.Burst:
                    BurstAttack(enemiesInRange[0]);
                    break;
                
                case DamageType.NotApplicable:
                    NotApplicableAttack();
                    break;
                    



                /*case DamageType.Multiple:
                    AttackMultiple(3); // Par exemple, attaque jusqu'� 3 cibles
                    break;

                case DamageType.Random:
                    Attack(enemiesInRange[Random.Range(0, enemiesInRange.Count)]);
                    break;

                case DamageType.Closest:
                    Attack(GetClosestEnemy());
                    break;

                case DamageType.Farthest:
                    Attack(GetFarthestEnemy());
                    break;*/
            }
        }
    }

    private void ResetFireCooldown()
    {
        fireCooldown = data.baseFirerate; // R�initialiser le cooldown
    }


    #region Conditions
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
        //Debug.Log($"{obj} est rentrer dans la zone pour etre check");
        return obj.GetComponent<Enemy>();
        //return ((1 << obj.layer) & enemyLayer) != 0;
    }

    private bool IsInRange(Transform target)
    {
        return enemiesInRange.Contains(target.gameObject);
    }

    private GameObject GetClosestEnemy()
    {
        return enemiesInRange.OrderBy(e => Vector2.Distance(transform.position, e.transform.position)).FirstOrDefault();
    }

    private GameObject GetFarthestEnemy()
    {
        return enemiesInRange.OrderByDescending(e => Vector2.Distance(transform.position, e.transform.position)).FirstOrDefault();
    }
    #endregion
    
    #region AttackType
    private void AttackMultiple(int count)
    {
        var targets = enemiesInRange.Take(count); // S�lectionner les X premi�res cibles
        foreach (var target in targets)
        {
            SingleAttack(target);
        }
    }

    private void Attack(GameObject enemyTarget)
    {
        //Debug.Log($"{gameObject} Shouting");
        Enemy enemyScript = enemyTarget.GetComponent<Enemy>();
        Assert.IsNotNull(enemyScript, $"L'objet {enemyTarget.name} n'a pas de script Enemy attach�.");
        
        if (data.damageType != DamageType.NotApplicable)
            transform.LookAt(new Vector3(enemyScript.transform.position.x, transform.position.y, enemyScript.transform.position.z));
        enemyScript.TakeDamage(data.baseDamage);
        ResetFireCooldown();
        
        //Play Sound
        //AudioManager.instance.PlaySfx(data.attackSound);
    }

    private void SingleAttack(GameObject enemyTarget)
    {
        Attack(enemyTarget);
    }
    
    private void BurstAttack(GameObject enemyTarget)
    {
        if (!enemyTarget) return;
        if (burstCount > 0) return;
        StartCoroutine(PerformBurstAttack(enemyTarget));
    }
    private IEnumerator PerformBurstAttack(GameObject enemyTarget)
    {
        burstCount = data.burstMaxBullets;
        for (int i = 0; i < data.burstMaxBullets; i++)
        {
            if (!enemyTarget) yield break;
            burstCount--;
            Attack(enemyTarget);
            
            ////Play Sound
            //AudioManager.instance.PlaySfx(data.attackSound);
            
            yield return new WaitForSeconds(data.burstDelay);
        }
    }

    private void NotApplicableAttack()
    {
        switch (data.tower)
        {
            case TowerClass.Patrol:
                //GetComponent<PatrolSpawner>().Spawn();
                //EventManager.instance.onSpawnPatrol?.Invoke();
                break;
        }
    }
    
    #endregion
}