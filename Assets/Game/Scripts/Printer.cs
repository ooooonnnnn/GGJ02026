using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Printer : MonoBehaviour
{
    public void Print(string message)
    {
        print(message);
    }

    public void AttachToInteractor(SelectEnterEventArgs args)
    {
        print(args.interactorObject.GetAttachTransform(args.interactableObject).position);
        
        // Destroy(args.interactorObject.transform.gameObject);
        
        // args.interactableObject.transform.SetParent(
        //     args.interactorObject.GetAttachTransform());
    }
}
