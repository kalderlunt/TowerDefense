using Assets.Scripts.Data;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Towers.Patrol;
using UnityEngine;

namespace Assets.Scripts.Spawners
{ 
    public class PatrolSpawner : MonoBehaviour, ISpawner
    {
        [SerializeField] private PatrolData data;
        [SerializeField] private Transform parentStorage;
        //private Dictionary<GameObject, ComponentPool<Patrol>> patrolPools;

        private void Awake()
        {
            //patrolPools = new Dictionary<GameObject, ComponentPool<Patrol>>();
            
            /*patrolPools[data.patrolPrefab] = new ComponentPool<Patrol>(
                prefab: data.patrolPrefab,
                capacity: 6,
                preAllocateCount: 6, // Pre-allocation
                parentStorage: parentStorage
            );*/
        }

        public void Spawn()
        {
            //Patrol patrol = patrolPools[data.patrolPrefab].Get();
            //patrol.onDeath += () => patrolPools[data.patrolPrefab].Release(patrol);
            /*patrol.onDestroyPatrol += () =>
            {
                Destroy(patrol.gameObject);
            };*/
            
            Patrol patrol = Instantiate(data.patrolPrefab, parentStorage).GetComponent<Patrol>();
            patrol.SetData(data);
            patrol.ResetData(data);
        }
    }
}