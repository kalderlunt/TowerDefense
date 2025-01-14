using System;
using UnityEngine;

public class TowerRange : MonoBehaviour
{
    public Action<Collider> EventEnterInRange;
    public Action<Collider> EventExitInRange;
    private SphereCollider rangeCollider;

    private void OnEnable()
    {
        rangeCollider = GetComponent<SphereCollider>();
        rangeCollider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        EventEnterInRange?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        EventExitInRange?.Invoke(other);
    }
}