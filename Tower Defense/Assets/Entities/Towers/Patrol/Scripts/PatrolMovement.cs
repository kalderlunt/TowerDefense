using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Towers.Patrol
{
    public class PatrolMovement : MonoBehaviour
    {
        private Patrol patrol;
        private PatrolData data;
        private Transform[] waypoints;
        private int currentWaypointIndex;

        private void Start()
        {
            patrol = GetComponent<Patrol>();
            data = GetComponent<Patrol>().GetData();
        }

        private void OnEnable()
        {
            Debug.Log("Enable PatrolMovement");
            waypoints = WaypointManager.instance.waypoints;
            currentWaypointIndex = waypoints.Length - 1;
            transform.position = waypoints[currentWaypointIndex].position;
        }

        private void Update()
        {
            if (currentWaypointIndex < 0)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 targetPosition = new (waypoints[currentWaypointIndex].position.x, transform.position.y, waypoints[currentWaypointIndex].position.z);
            
            transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, data.moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                currentWaypointIndex--;
            }
        }
    }
}