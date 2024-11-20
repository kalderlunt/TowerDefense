using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 2f;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private void Start()
    {
        waypoints = WaypointManager.Instance.Waypoints;
        transform.position = waypoints[currentWaypointIndex].position;
    }

    private void Update()
    {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 targetPosition = new (waypoints[currentWaypointIndex].position.x, transform.position.y, waypoints[currentWaypointIndex].position.z);

        transform.transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}