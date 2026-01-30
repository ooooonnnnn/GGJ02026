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
        
        grabInteractable.firstSelectEntered.AddListener(EnableCapGrab);
        grabInteractable.lastSelectExited.AddListener(DisableCapGrab);
        
        capGrabInteractable.firstSelectEntered.AddListener(CapRemoved);
    }

    private GameObject handInRangeObj;

    private void EnableCapGrab(SelectEnterEventArgs args)
    {
        //if capped, allow cap to be grabbed
        if (capped) capGrabInteractable.enabled = true;
    }

    private void DisableCapGrab(SelectExitEventArgs args)
    {
        //if capped, disable cap grab
        if (capped) capGrabInteractable.enabled = false;
    }

    private void CapRemoved(SelectEnterEventArgs args)
    {
        if (capped)
        {
            capped = false;
            grabInteractable.firstSelectEntered.RemoveListener(EnableCapGrab);
            grabInteractable.lastSelectExited.RemoveListener(DisableCapGrab);
        }
    }
}
