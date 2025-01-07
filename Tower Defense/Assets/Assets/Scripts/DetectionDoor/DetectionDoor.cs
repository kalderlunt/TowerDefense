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
        EnemyData enemy = other.GetComponent<EnemyData>();
        if (enemy)
        {
            playerHealth.DecreaseHealth(enemy.health);
            Destroy(enemy.gameObject);
        }
    }
}