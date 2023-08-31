using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInteractUI : MonoBehaviour {

    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

    private void Update() {
        if (playerInteract.GetInteractableObject() != null && !playerInteract.GetInteractionState()) {
            Show(playerInteract.GetInteractableObject());
        } else {
            Hide();
        }
    }

    private void Show(IInteractable interactable) {
        containerGameObject.SetActive(true);
        interactTextMeshProUGUI.text = interactable.GetInteractText();
        if (interactable.GetInteractText() != "PHONE")
        {
            containerGameObject.transform.position = interactable.GetTransform().position + Vector3.up * 2.5f;
            containerGameObject.transform.rotation = interactable.GetTransform().rotation * Quaternion.Euler(0, 180, 0);
        }
    }


    private void Hide() {
        containerGameObject.SetActive(false);
    }

}