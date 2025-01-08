using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Towers.Patrol
{
    public class PatrolMovement : MonoBehaviour
    {
        private PatrolData data;
        private Transform[] waypoints;
        private int currentWaypointIndex;

        private void Start()
        {
            data = GetComponent<Patrol>().GetData();
            data.transform = transform;
        }

        private void OnEnable()
        {
            Debug.Log("Enable PatrolMovement");
            waypoints = WaypointManager.instance.waypoints;
            currentWaypointIndex = waypoints.Length;
            transform.position = waypoints[currentWaypointIndex].position;
        }

        private void Update()
        {
            if (currentWaypointIndex <= waypoints.Length) return;

            Vector3 targetPosition = new (waypoints[currentWaypointIndex].position.x, data.transform.position.y, waypoints[currentWaypointIndex].position.z);

            data.transform.LookAt(new Vector3(targetPosition.x, data.transform.position.y, targetPosition.z));
            transform.position = Vector3.MoveTowards(data.transform.position, targetPosition, data.moveSpeed * Time.deltaTime);

            if (Vector3.Distance(data.transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex--;
            }
        }
    }
}