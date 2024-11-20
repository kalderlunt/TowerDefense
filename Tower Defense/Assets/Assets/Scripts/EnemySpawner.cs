using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float SpawnInterval = 2f; 
    public int EnemiesPerWave = 5;
    private int enemiesSpawned = 0;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, SpawnInterval);
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= EnemiesPerWave)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        enemiesSpawned++;
    }
}