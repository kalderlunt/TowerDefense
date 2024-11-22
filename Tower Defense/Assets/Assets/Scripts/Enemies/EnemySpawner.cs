using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> Waves;
    [SerializeField] private float WaveInterval = 30f;

    private Dictionary<GameObject, ComponentPool<Enemy>> enemyPools; // Pools basés sur des piles
    private int currentWaveIndex = 0;

    private void Start()
    {
        enemyPools = new Dictionary<GameObject, ComponentPool<Enemy>>();

        foreach (EnemyWave wave in Waves)
        {
            foreach (EnemyConfig config in wave.enemies)
            {
                if (enemyPools.ContainsKey(config.enemyPrefab))
                {
                    continue;
                }

                enemyPools[config.enemyPrefab] = new ComponentPool<Enemy>(
                    config.enemyPrefab,
                    capacity: 50,
                    preAllocateCount: 50 // Pré-allocation initiale
                );
            }
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < Waves.Count)
        {
            EnemyWave currentWave = Waves[currentWaveIndex];
            Debug.Log($"Vague {currentWaveIndex + 1} : {currentWave.waveName}");

            yield return StartCoroutine(SpawnWaveEnemies(currentWave));
            yield return new WaitForSeconds(WaveInterval); // peut etre remplace par un bouton ou trigger

            currentWaveIndex++;
        }

        Debug.Log("Toutes les vagues ont été complétées !");
    }

    private IEnumerator SpawnWaveEnemies(EnemyWave wave)
    {
        foreach (EnemyConfig config in wave.enemies)
        {
            yield return StartCoroutine(SpawnEnemyType(config));
        }
    }

    private IEnumerator SpawnEnemyType(EnemyConfig config)
    {
        for (int i = 0; i < config.count; i++)
        {
            //Instantiate(config.enemyPrefab, transform.position, Quaternion.identity); // A CHANGER PAR LE POOL SYSTEM

            Enemy enemy = enemyPools[config.enemyPrefab].Get();
            
            //remettre ses data a 0
            enemy.ResetData();

            // Relâcher l'ennemi dans le pool après sa mort
            enemy.onDeath += () => enemyPools[config.enemyPrefab].Release(enemy);

            yield return new WaitForSeconds(config.spawnInterval);
        }
    }


    /*    public GameObject enemyPrefab;
        public float spawnInterval = 0.5f; 
        public int enemiesPerWave = 5;
        private int enemiesSpawned = 0;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
        }

        private void SpawnEnemy()
        {
            if (enemiesSpawned >= enemiesPerWave)
            {
                CancelInvoke(nameof(SpawnEnemy));
                return;
            }

            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            enemiesSpawned++;
        }*/
}