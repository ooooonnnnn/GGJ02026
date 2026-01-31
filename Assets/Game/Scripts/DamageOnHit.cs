using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private bool destroyOnAnyHit;

    private void OnCollisionEnter(Collision other)
    {
        //check if the collider is an enemy
        EnemyAI target = other.gameObject.GetComponent<EnemyAI>();
        if (target)
        {
            target.Hit();
            print("Take that bitch");
            Destroy(gameObject);
        }
        
        if (destroyOnAnyHit) Destroy(gameObject);
    }
}
