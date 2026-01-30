using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SodaBottle : MonoBehaviour
{
    private bool capped = true;
    private bool fizzing;

    [SerializeField] private GunShooting gun;
    [SerializeField, ] private BottleCap cap;
    [SerializeField, ] private XRGrabInteractable grabInteractable;
    [SerializeField] private XRGrabInteractable capGrabInteractable;

    private void OnValidate()
    {
        gun = GetComponent<GunShooting>();
        cap = GetComponentInChildren<BottleCap>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        
        capGrabInteractable = cap.GetComponent<XRGrabInteractable>();
        capGrabInteractable.enabled = false;
        
        grabInteractable.firstSelectEntered.AddListener(HandleGrabbed);
        grabInteractable.lastSelectExited.AddListener(HandleDropped);
    }

    private GameObject handInRangeObj;

    private void HandleGrabbed(SelectEnterEventArgs args)
    {
        //if capped, allow cap to be grabbed
        if (capped) capGrabInteractable.enabled = true;
    }

    private void HandleDropped(SelectExitEventArgs args)
    {
        //if capped, disable cap grab
        if (capped) capGrabInteractable.enabled = false;
    }

    private void CapRemoved()
    {
        capped = false;
    }
}
