using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BottleCap : MonoBehaviour
{
    [SerializeField, HideInInspector] private XRSimpleInteractable interactable;
    [SerializeField, HideInInspector] private Rigidbody rb;

    private void OnValidate()
    {
        interactable = GetComponent<XRSimpleInteractable>();
        rb = GetComponent<Rigidbody>();
    }

    public void MakeGrabbable() => interactable.enabled = true;
}
