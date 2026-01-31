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
    [SerializeField] private ColliderEvents colliderEvents;
    [SerializeField] private Transform capRoot;
    [SerializeField] private GameObject[] prompts;

    private void Awake()
    {
        gun = GetComponent<GunShooting>();
        cap = GetComponentInChildren<BottleCap>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        pokeInteractable = GetComponentInChildren<XRSimpleInteractable>();
        
        capGrabInteractable = cap.GetComponent<XRGrabInteractable>();
        capGrabInteractable.enabled = false;
        
        grabInteractable.firstSelectEntered.AddListener(EnableCapGrab);
        grabInteractable.lastSelectExited.AddListener(DisableCapGrab);
        
        capGrabInteractable.firstSelectEntered.AddListener(CapRemoved);
        
        pokeInteractable.firstSelectEntered.AddListener(TryShoot);
        
        UpdatePromptVisibility(-1);
    }

    private GameObject handInRangeObj;
    [SerializeField, Tooltip("Time it takes for the poke interaction to become enabled once the protocol is complete")]
    private float reloadToShootDelay;

    private void EnableCapGrab(SelectEnterEventArgs args)
    {
        //if capped, allow cap to be grabbed
        if (capped)
        {
            capGrabInteractable.enabled = true;
            UpdatePromptVisibility(0);
        }
    }

    private void UpdatePromptVisibility(int promptInd)
    {
        int i = 0;
        foreach (GameObject prompt in prompts)
        {
            prompt.SetActive(promptInd == i);
            i++;
        }
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
        grabInteractable.firstSelectEntered.RemoveListener(EnableCapGrab);
        grabInteractable.lastSelectExited.RemoveListener(DisableCapGrab);

        colliderEvents.TriggerEnter += ItemPresentedToBottle;
        
        UpdatePromptVisibility(1);
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
                
                
                UpdatePromptVisibility(2);
            }
        }
        
        //look for cap
        if (!capped && fizzing)
        {
            if (other.CompareTag(bottleCapTag))
            {
                gun.Reload();
                capped = true;
                
                Transform capTransform = other.transform;
                capTransform.GetComponent<XRGrabInteractable>().enabled = false;
                
                capTransform.SetParent(capRoot);
                capTransform.localPosition = Vector3.zero;
                capTransform.localRotation = Quaternion.identity;
                capTransform.GetComponent<Rigidbody>().isKinematic = true;
                
                Invoke(nameof(EnablePoke), reloadToShootDelay);
            }
        }
    }

    private void EnablePoke()
    {
        pokeInteractable.enabled = true;
        UpdatePromptVisibility(3);
    }

    private void TryShoot(SelectEnterEventArgs args)
    {
        if (fizzing && capped)
        {
            gun.OnShoot.AddListener(DestroyCap);
            gun.Shoot();
            
            fizzing = false;
            capped = false;
            UpdatePromptVisibility(1);
        }
    }

    private void DestroyCap()
    {
        Destroy(cap.gameObject);
    }
}
