using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DropZoneManager : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor socketInteractor;
    private bool objectIn = false;

    private void Activate()
    {
        socketInteractor.enabled = true;
    }
    private void Deactivate()
    {
        socketInteractor.enabled = false;
    }

    public void SelectEnter()
    {
        objectIn = true;
    }

    public void SelectExit()
    {
        objectIn = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "RightHand")
        {
            Activate();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RightHand" && !objectIn)
        {
            Deactivate();
        }
    }
}
