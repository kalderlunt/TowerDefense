using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public string waveName;
    public List<EnemyConfig> enemies;
}

[System.Serializable]
public class EnemyConfig
{
    public GameObject enemyPrefab;
    public int count;
    public float spawnInterval;
}