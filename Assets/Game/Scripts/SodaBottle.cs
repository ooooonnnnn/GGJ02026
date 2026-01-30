using System;
using UnityEngine;

public class SodaBottle : MonoBehaviour
{
    private bool capped;
    private bool fizzing;

    [SerializeField, HideInInspector] private GunShooting gun;
    [SerializeField, HideInInspector] private BottleCap cap;

    private void OnValidate()
    {
        gun = GetComponent<GunShooting>();
        cap = GetComponentInChildren<BottleCap>();
        
        HandleSelection selection = GetComponent<HandleSelection>();
        selection.OnGrabbed.AddListener(cap.MakeGrabbable);
    }
}
