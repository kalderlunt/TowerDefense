using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> Waves;
    [SerializeField] private float WaveInterval = 30f;

    private int currentWaveIndex = 0;
    private void Start()
    {
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
            Instantiate(config.enemyPrefab, transform.position, Quaternion.identity); // A CHANGER PAR LE POOL SYSTEM

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