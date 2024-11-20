using UnityEngine;

[RequireComponent(typeof(EnemyData))]
public class EnemyMovement : MonoBehaviour
{
    private EnemyData data;
    private Transform[] waypoints;
    private int currentWaypointIndex;

    private void OnEnable()
    {
        data = GetComponent<EnemyData>();
        data.transform = transform;
        waypoints = WaypointManager.instance.waypoints;
        currentWaypointIndex = 0;
        transform.position = waypoints[currentWaypointIndex].position;
    }

    private void Update()
    {
        if (currentWaypointIndex >= waypoints.Length) return;

        Vector3 targetPosition = new (waypoints[currentWaypointIndex].position.x, data.transform.position.y, waypoints[currentWaypointIndex].position.z);

        data.transform.LookAt(new Vector3(targetPosition.x, data.transform.position.y, targetPosition.z));
        transform.position = Vector3.MoveTowards(data.transform.position, targetPosition, data.moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex++;
        }
    }
}