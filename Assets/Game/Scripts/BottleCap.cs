using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BottleCap : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable interactable;
    [SerializeField, HideInInspector] private Rigidbody rb;

    private void Awake()
    {
        interactable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // interactable.enabled = false;
        // rb.isKinematic = true;
        
        interactable.lastSelectExited.AddListener(RBNotKinematic);
    }

    private void RBNotKinematic(SelectExitEventArgs args)
    {
        rb.isKinematic = false;
    }
    
}
