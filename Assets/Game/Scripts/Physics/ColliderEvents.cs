using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderEvents : MonoBehaviour
{
    public event Action<Collider> TriggerEnter;
    public event Action<Collider> TriggerExit;
    public event Action<Collision> CollisionEnter;
    
    private void OnTriggerEnter(Collider other)
    {
        TriggerEnter?.Invoke(other);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerExit?.Invoke(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        CollisionEnter?.Invoke(other);
    }
    
    private void OnDestroy()
    {
        TriggerEnter = null;
        TriggerExit = null;
        CollisionEnter = null;
    }
}
