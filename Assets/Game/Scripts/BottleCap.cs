using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BottleCap : MonoBehaviour
{
    [SerializeField, HideInInspector] private XRSimpleInteractable interactable;
    [SerializeField, HideInInspector] private Rigidbody rb;
    [SerializeField, HideInInspector] private HandleSelection selection;
    public HandleSelection getSelectionScript => selection;

    private void OnValidate()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        rb = GetComponent<Rigidbody>();
        selection = GetComponent<HandleSelection>();

        interactable.enabled = false;
        rb.isKinematic = true;
    }

    public void MakeGrabbable() => interactable.enabled = true;
}
