using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Towers.Patrol;
using UnityEngine;

namespace Assets.Scripts.Spawners
{ 
    [RequireComponent(typeof(PatrolData))]
    public class SpawnerPatrol : MonoBehaviour, ISpawner
    {
        [SerializeField] private PatrolData data;
        [SerializeField] private GameObject patrolPrefab ;
        private Transform parentStorage;
        private Dictionary<GameObject, ComponentPool<PatrolBehaviour>> patrolPools;

        private void Start()
        {
            patrolPools = new Dictionary<GameObject, ComponentPool<PatrolBehaviour>>();
            parentStorage = transform;

            patrolPools[data.patrolPrefab] = new ComponentPool<PatrolBehaviour>(
                prefab: data.patrolPrefab,
                capacity: 50,
                preAllocateCount: 50, // Pr�-allocation initiale
                parentStorage: this.parentStorage
            );        
        }

        public void Spawn()
        {
            PatrolBehaviour patrol = patrolPools[data.patrolPrefab].Get();
            patrol.ResetData();
            patrol.onDeath += () => patrolPools[data.patrolPrefab].Release(patrol);
        }
    }
}