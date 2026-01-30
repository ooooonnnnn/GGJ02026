using System;
using UnityEngine;

public class SodaBottle : MonoBehaviour
{
    private bool capped;
    private bool fizzing;

    [SerializeField, HideInInspector] private GunShooting gun;
    [SerializeField, HideInInspector] private BottleCap cap;
    [SerializeField, HideInInspector] private HandleSelection capSelectionScript;

    private void OnValidate()
    {
        gun = GetComponent<GunShooting>();
        cap = GetComponentInChildren<BottleCap>();
        
        // HandleSelection selection = GetComponent<HandleSelection>();
        // selection.OnGrabbed.AddListener(cap.MakeGrabbable);
        // capSelectionScript = cap.GetComponent<HandleSelection>();
        // capSelectionScript.OnGrabbed.AddListener(CapRemoved);

        // ColliderEvents capColliderEvents = cap.GetComponent<ColliderEvents>();
        // capColliderEvents.TriggerEnter += OnObjectEntersCapCol;
        // capColliderEvents.TriggerExit += OnObjectExitCapCol;
    }

    private GameObject handInRangeObj;

    private void OnObjectEntersCapCol(Collider other)
    {
    }

    private void OnObjectExitCapCol(Collider other)
    {
        
    }

    private void CapRemoved()
    {
        capped = false;
        capSelectionScript.OnGrabbed.RemoveListener(CapRemoved);
    }
}
