using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// Can be grabbed and released with hands by pinching. Toggles, no contniuous interaction required
/// </summary>
[RequireComponent(typeof(XRSimpleInteractable))]
public class ToggleGrabAndRelease : MonoBehaviour
{
    /// <summary>
    /// None => not grabbed. right or left => grabbed
    /// </summary>
    private InteractorHandedness grabbedByHand = InteractorHandedness.None;
    
    private Rigidbody rb;

    private void OnValidate()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        interactable.selectEntered.AddListener(HandleSelect);
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
    }

    private void HandleSelect(SelectEnterEventArgs args)
    {
        InteractorHandedness selectingHand = args.interactorObject.handedness;

        if (grabbedByHand is InteractorHandedness.None) Grab(selectingHand);
        
        else if (grabbedByHand == selectingHand) Release();
    }

    private void Grab(InteractorHandedness grabbingHand)
    {
        grabbedByHand = grabbingHand;
        
        Transform grabbingTransform = grabbingHand == InteractorHandedness.Right ? 
            HandManager.instance.RightPalm : HandManager.instance.LeftPalm;
        transform.SetParent(grabbingTransform);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        
        rb.isKinematic = true;
    }

    private void Release()
    {
        grabbedByHand = InteractorHandedness.None;
        
        transform.SetParent(null);
        
        rb.isKinematic = false;
    }
}
