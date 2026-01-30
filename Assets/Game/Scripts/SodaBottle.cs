using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SodaBottle : MonoBehaviour
{
    private const string mentosTag = "Mentos";
    private const string bottleCapTag = "Bottle Cap";
    private bool capped = true;
    private bool fizzing = false;

    [SerializeField, HideInInspector] private GunShooting gun;
    [SerializeField, HideInInspector] private BottleCap cap;
    [SerializeField, HideInInspector] private XRGrabInteractable grabInteractable;
    [SerializeField, HideInInspector] private XRSimpleInteractable pokeInteractable;
    [SerializeField, HideInInspector] private XRGrabInteractable capGrabInteractable;
    [SerializeField, HideInInspector] private ColliderEvents colliderEvents;
    [SerializeField] private Transform capRoot;

    private void OnValidate()
    {
        gun = GetComponent<GunShooting>();
        cap = GetComponentInChildren<BottleCap>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        colliderEvents = GetComponentInChildren<ColliderEvents>();
        pokeInteractable = GetComponentInChildren<XRSimpleInteractable>();
        
        capGrabInteractable = cap.GetComponent<XRGrabInteractable>();
        capGrabInteractable.enabled = false;
        
        grabInteractable.firstSelectEntered.AddListener(EnableCapGrab);
        grabInteractable.lastSelectExited.AddListener(DisableCapGrab);
        
        capGrabInteractable.firstSelectEntered.AddListener(CapRemoved);
        
        pokeInteractable.firstSelectEntered.AddListener(TryShoot);
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
        if (!capped) return;
        
        capped = false;
        print("Cap Removed");
        grabInteractable.firstSelectEntered.RemoveListener(EnableCapGrab);
        grabInteractable.lastSelectExited.RemoveListener(DisableCapGrab);

        colliderEvents.TriggerEnter += ItemPresentedToBottle;
    }

    private void ItemPresentedToBottle(Collider other)
    {
        
        //look for mentos
        if (!capped && !fizzing)
        {
            if (other.CompareTag(mentosTag))
            {
                Destroy(other.gameObject);
                fizzing = true;
            }
        }
        
        //look for cap
        if (!capped && fizzing)
        {
            if (other.CompareTag(bottleCapTag))
            {
                gun.Reload();
                capped = true;
                
                Transform capTransform = other.transform.parent;
                capTransform.GetComponent<XRGrabInteractable>().enabled = false;
                
                capTransform.SetParent(capRoot);
                capTransform.localPosition = Vector3.zero;
                capTransform.localRotation = Quaternion.identity;
                capTransform.GetComponent<Rigidbody>().isKinematic = true;
                
                pokeInteractable.enabled = true;
            }
        }
    }

    private void TryShoot(SelectEnterEventArgs args)
    {
        if (fizzing && capped)
        {
            gun.OnShoot.AddListener(DestroyCap);
            gun.Shoot();
        }
    }

    private void DestroyCap()
    {
        Destroy(cap.gameObject);
    }
}
