using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SodaBottle : MonoBehaviour
{
    private bool capped;
    private bool fizzing;

    [SerializeField, HideInInspector] private GunShooting gun;
    [SerializeField, HideInInspector] private BottleCap cap;
    [SerializeField, HideInInspector] private HandleSelection capSelectionScript;
    [SerializeField, HideInInspector] private XRGrabInteractable grabInteractable;

    private void OnValidate()
    {
        gun = GetComponent<GunShooting>();
        cap = GetComponentInChildren<BottleCap>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        grabInteractable.firstSelectEntered.AddListener(alwidhj);
        
        // HandleSelection selection = GetComponent<HandleSelection>();
        // selection.OnGrabbed.AddListener(cap.MakeGrabbable);
        // capSelectionScript = cap.GetComponent<HandleSelection>();
        // capSelectionScript.OnGrabbed.AddListener(CapRemoved);

        // ColliderEvents capColliderEvents = cap.GetComponent<ColliderEvents>();
        // capColliderEvents.TriggerEnter += OnObjectEntersCapCol;
        // capColliderEvents.TriggerExit += OnObjectExitCapCol;
    }

    private GameObject handInRangeObj;

    private void alwidhj(SelectEnterEventArgs args)
    {
    }
    
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
