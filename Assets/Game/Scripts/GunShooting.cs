using UnityEngine;
using UnityEngine.Events;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private int clipSize;
    [SerializeField] private int currentAmmo;

    [SerializeField] public UnityEvent OnShoot;
    
    public void Shoot()
    {
        if (currentAmmo <= 0) return;
        currentAmmo--;
        print("shoot");
        OnShoot.Invoke();
    }
    
    public void Reload()
    {
        print("reload");
        currentAmmo = clipSize;
    }
}
