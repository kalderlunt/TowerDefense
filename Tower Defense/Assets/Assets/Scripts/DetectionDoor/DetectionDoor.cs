using UnityEngine;

public class DetectionDoor : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    private Collider collider;

    private void Awake()
    {
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyData enemyData = other.GetComponent<EnemyData>();
        if (enemyData)
        {
            Enemy enemy = enemyData.GetComponent<Enemy>();
            playerHealth.DecreaseHealth(enemyData.health);
            enemy.TakeDamage(enemyData.health);
            Destroy(enemyData.gameObject);
        }
    }
}