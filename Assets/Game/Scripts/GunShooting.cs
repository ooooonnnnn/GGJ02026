using UnityEngine;
using UnityEngine.Events;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private int clipSize;
    [SerializeField] private int currentAmmo;

    [SerializeField] private UnityEvent OnShoot;
    
    public void Shoot()
    {
        print("shoot");
        OnShoot.Invoke();
    }
    
    public void Reload()
    {
        print("reload");
        currentAmmo = clipSize;
    }
}
