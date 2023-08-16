using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour {

    [SerializeField] private VoiceExperience voiceExperience;
    [SerializeField] private InputActionProperty activateButton;
    private bool inInteraction = false;
    private IInteractable interactableActual;

    private void Update() {

        if (activateButton.action.triggered && !inInteraction) {
            interactableActual = GetInteractableObject();
            if (interactableActual != null) {
                voiceExperience.SetInteractable(interactableActual);
                StartCoroutine(interactableActual.Interact(0));
                inInteraction = true;
            }
        }

        if (inInteraction && GetInteractableObject() != interactableActual)
        {
            interactableActual.StopDistance();
            inInteraction = false;
        }
    }

    public void NotInteractionState()
    {
        inInteraction= false;
    }

    public bool GetInteractionState()
    {
        return inInteraction;
    }

    public IInteractable GetInteractableObject() {
        List<IInteractable> interactableList = new List<IInteractable>();
        float interactRange = 4f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        if (colliderArray.Length == 0)
        {
            return null;
        }
        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactableList.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        foreach (IInteractable interactable in interactableList)
        {
            if (closestInteractable == null)
            {
                closestInteractable = interactable;
            }
            else
            {
                if (Vector3.Distance(transform.position, interactable.GetTransform().position) <
                    Vector3.Distance(transform.position, closestInteractable.GetTransform().position))
                {
                    // Closer
                    closestInteractable = interactable;
                }
            }
        }

        return closestInteractable;
    }



}