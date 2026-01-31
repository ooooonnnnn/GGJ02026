using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    [SerializeField] private float damageAmount;

    private void OnCollisionEnter(Collision other)
    {
        //check if the collider is an enemy
        if (true)
        {
            EnemyAI target = other.gameObject.GetComponent<EnemyAI>();
            if(target != null){
                target.Hit();
            }
            Destroy(gameObject);
        }
        
        Destroy(gameObject);
    }
}
