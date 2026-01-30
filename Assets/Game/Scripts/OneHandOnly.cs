using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRGrabInteractable))]
public class OneHandOnly : MonoBehaviour
{
    private const string leftHandLayer = "Left Controller";
    private const string rightHandLayer = "Right Controller";
    [SerializeField, HideInInspector] private XRGrabInteractable grabInteractable;
    

    private void OnValidate()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.firstSelectEntered.AddListener(BecomeOwnedByHand);
        grabInteractable.lastSelectExited.AddListener(BecomeUnowned);
    }

    private void BecomeOwnedByHand(SelectEnterEventArgs args)
    {
        InteractorHandedness grabbingHand = args.interactorObject.handedness;
        grabInteractable.interactionLayers = grabbingHand == InteractorHandedness.Left
            ? InteractionLayerMask.GetMask(leftHandLayer)
            : InteractionLayerMask.GetMask(rightHandLayer);
    }

    private void BecomeUnowned(SelectExitEventArgs args)
    {
        grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Everything");
    }
}
