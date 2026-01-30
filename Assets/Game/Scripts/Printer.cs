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

        Transform grabbed = args.interactableObject.transform;
        Transform grabbingHand = args.interactorObject.handedness == InteractorHandedness.Right ?
            HandManager.instance.RightPalm :
            HandManager.instance.LeftPalm;

        grabbed.SetParent(grabbingHand);
    }
}
