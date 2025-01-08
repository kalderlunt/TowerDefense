using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Towers.Patrol;
using UnityEngine;

namespace Assets.Scripts.Spawners
{ 
    public class PatrolSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private PatrolData data;
        private Patrol patrol;
        private Transform parentStorage;
        private Dictionary<GameObject, ComponentPool<Patrol>> patrolPools;

        private void Start()
        {
            patrolPools = new Dictionary<GameObject, ComponentPool<Patrol>>();
            parentStorage = transform;

            patrolPools[data.patrolPrefab] = new ComponentPool<Patrol>(
                prefab: data.patrolPrefab,
                capacity: 6,
                preAllocateCount: 6, // Pre-allocation
                parentStorage: this.parentStorage
            );
        }

        public void Spawn()
        {
            Debug.Log("Patrol Spawned");
            patrol = patrolPools[data.patrolPrefab].Get();
            patrol.ResetData(data);
            patrol.onDeath += () => patrolPools[data.patrolPrefab].Release(patrol);
        }
    }
}