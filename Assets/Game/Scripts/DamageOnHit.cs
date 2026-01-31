using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private bool destroyOnAnyHit;
    public UnityEvent OnDamagingHit;
    [SerializeField] private float destroyTime;

    private void OnCollisionEnter(Collision other)
    {
        //check if the collider is an enemy
        EnemyAI target = other.gameObject.GetComponent<EnemyAI>();
        if (target)
        {
            target.Hit();
            print("Take that bitch");
            OnDamagingHit.Invoke();
            DisableChildren();
            Invoke(nameof(SelfDestruct), destroyTime);
        }
        
        if (destroyOnAnyHit)
        {
            DisableChildren();
            Invoke(nameof(SelfDestruct), destroyTime);
        }

    }

    private void DisableChildren()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
