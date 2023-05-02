using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void FixedUpdate()
    {
        transform.position = target.position
            + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        transform.eulerAngles = new Vector3(0,target.eulerAngles.y,0);
    }

    public InputActionReference buttonAInput;
    [SerializeField] private GameObject obj;
    private bool etat = false;

    void Update()
    {
        if (buttonAInput.action.triggered && etat == false)
        {
            etat = !etat;
            offset.y -= 100;
        }
        else if (buttonAInput.action.triggered && etat == true)
        {
            etat = !etat;
            offset.y += 100;
        }
    }
}
