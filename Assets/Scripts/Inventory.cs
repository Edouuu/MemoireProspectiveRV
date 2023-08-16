using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InputActionReference inputGrabLeft;
    [SerializeField] private InputActionReference inputActivateLeft;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Vector3 offset_base;
    bool bool_collision = false;
    bool activated = false;

    private void Start()
    {
        offset_base = offset;
    }

    void Update()
    {
        if (bool_collision && (inputGrabLeft.action.ReadValue<float>() >= 0.1f || inputActivateLeft.action.ReadValue<float>() >= 0.1f))
        {
            activated = true;
        }
        else if (activated && inputGrabLeft.action.ReadValue<float>() == 0.0f && inputActivateLeft.action.ReadValue<float>() == 0.0f)
        {
            activated = false;
        }

        if (activated)
        {
            offset = offset_base;
        }
        else
        {
            offset.y = offset_base.y -100f;
        }
    }

    void FixedUpdate()
    {
        inventory.transform.position = target.position
            + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;

        //inventory.transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        inventory.transform.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, 0);
        //inventory.transform.eulerAngles = new Vector3(target.eulerAngles.x +90, target.eulerAngles.y, target.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftHand" || other.gameObject.name == "RightHand")
        {
            bool_collision = true;
            //Debug.Log("CollideLeftHand");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "LeftHand" || other.gameObject.name == "RightHand")
        {
            bool_collision = false;
        }
    }
}
