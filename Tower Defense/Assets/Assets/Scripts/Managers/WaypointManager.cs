using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager instance;
    [HideInInspector] public Transform[] waypoints;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        WaypointInitialize();
    }

    private void WaypointInitialize()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
    }
}