using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SafeGrabOnly : MonoBehaviour
{

    public Transform allowedGrabZone;
    public float maxGrabDistance = 0.1f;

    private IXRSelectInteractable grabInteractable;
    private XRInteractionManager interactionManager;
    void Awake()
    {
        grabInteractable = GetComponent<IXRSelectInteractable>();
        grabInteractable.selectEntered.AddListener(CheckGrabLocation);
    }

    private void CheckGrabLocation(SelectEnterEventArgs args)
    {
        // Position der Hand (Interactor)
        Transform interactorTransform = args.interactorObject.transform;

        // Abstand zur erlaubten Zone (Griff)
        float distance = Vector3.Distance(interactorTransform.position, allowedGrabZone.position);

        if (distance > maxGrabDistance)
        {
            // Zu weit vom Griff entfernt - abbrechen
            var interactor = args.interactorObject;
            var interactable = args.interactableObject;

            if (interactor is IXRSelectInteractor selectInteractor && interactable is IXRSelectInteractable selectInteractable)
            {
               interactionManager.SelectExit(args.interactorObject, args.interactableObject);
            }
        }
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(CheckGrabLocation);
    }
}

