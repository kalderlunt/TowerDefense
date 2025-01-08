using UnityEngine;


public class EnemyData : MonoBehaviour
{
    [HideInInspector] public float health;
    public float maxHealth;

    public float moveSpeed;
    [HideInInspector] public Transform transform;

    public void ResetData()
    {
        health = maxHealth;
    }
}