using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [System.Serializable]
    public class PatrolData
    {
        public GameObject patrolPrefab; // avec les niveaux 
        //public List<GameObject> patrolPrefabs; // avec les niveaux 
        
        [HideInInspector] public float health;
        public float maxHealth;
        
        public float patrolSpeed;
        [HideInInspector] public Transform transform;
        
        public void ResetData()
        {
            health = maxHealth;
        }
    }
}