using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GunShooting : MonoBehaviour
{
    [SerializeField] private int clipSize;
    private int currentAmmo = 0;
    [SerializeField] private Rigidbody bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed;

    [SerializeField] public UnityEvent OnShoot;
    
    public void Shoot()
    {
        if (currentAmmo <= 0) return;
        currentAmmo--;
        Rigidbody newBullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        newBullet.linearVelocity = transform.forward * bulletSpeed;
        OnShoot.Invoke();
    }
    
    public void Reload()
    {
        currentAmmo = clipSize;
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Reload();
            Shoot();
        }
    }
}
