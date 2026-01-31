using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SodaBottle2 : MonoBehaviour
{
    [SerializeField] private GameObject[] prompts;
    [SerializeField] private ColliderEvents itemDetectionColEvents;
    [SerializeField] private Transform capRoot;
    
    private BottleState state
    {
        get => _state;
        set
        {
            SetCallbacksForState(value);
            SetPromptForState(value);
            _state = value;
        }
    }

    private BottleState _state = BottleState.None;
    [SerializeField] private BottleCap cap;
    
    private enum BottleState
    {
        CLosedNoFizz,
        OpenNoFizz,
        OpenFizz,
        ClosedFizz,
        None
    }

    private void Awake()
    {
        state = DetectCapInChildren() ? BottleState.CLosedNoFizz : BottleState.OpenNoFizz;
    }

    private bool DetectCapInChildren()
    {
        cap = GetComponentInChildren<BottleCap>();

        if (cap) return true;
        return false;
    }

    private void SetCallbacksForState(BottleState newState)
    {
        switch (newState)
        {
            case BottleState.CLosedNoFizz:
                XRGrabInteractable capInteractable = cap.GetComponent<XRGrabInteractable>();
                capInteractable.firstSelectEntered.AddListener(HandleCapRemoved);
                break;
            case BottleState.OpenNoFizz:
                itemDetectionColEvents.TriggerEnter += ExpectMentos;
                break;
            case BottleState.OpenFizz:
                itemDetectionColEvents.TriggerEnter += ExpectCap;
                break;
        }
    }

    private void SetPromptForState(BottleState newState)
    {
        int index = newState switch
        {
            BottleState.CLosedNoFizz => 0,
            BottleState.OpenNoFizz => 1,
            BottleState.OpenFizz => 2,
            BottleState.ClosedFizz => 3,
            _ => -1
        };

        for (int i = 0; i < prompts.Length; i++)
        {
            prompts[i].SetActive(i == index);
        }
    }

    private void HandleCapRemoved(SelectEnterEventArgs args)
    {
        args.interactableObject.firstSelectEntered.RemoveListener(HandleCapRemoved);
        state = BottleState.OpenNoFizz;
    }
    
    private void ExpectMentos(Collider other)
    {
        if (!other.CompareTag("Mentos")) return;
        
        itemDetectionColEvents.TriggerEnter -= ExpectMentos;
        Destroy(other.gameObject);
        state = BottleState.OpenFizz;
    }

    private void ExpectCap(Collider other)
    {
        if (!other.CompareTag("Bottle Cap")) return;
        
        itemDetectionColEvents.TriggerEnter -= ExpectCap;
        
        //make it my child
        Transform capTransform = other.transform;
        capTransform.SetParent(capRoot);
        capTransform.localPosition = Vector3.zero;
        capTransform.localRotation = Quaternion.identity;
        
        //make it not interactable
        capTransform.GetComponent<XRGrabInteractable>().enabled = false;
        
        //make it kinematic
        capTransform.GetComponent<Rigidbody>().isKinematic = true;
        
        state = BottleState.ClosedFizz;
    }
}

