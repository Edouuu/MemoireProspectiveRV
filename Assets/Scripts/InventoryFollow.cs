using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class InventoryFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform _camera;
    [SerializeField] private CharacterController charController;
    [SerializeField] private Vector3 offset;

    void FixedUpdate()
    {

        Vector3 center = new Vector3(charController.center.z, 0.3f, -charController.center.x);
        transform.position = target.position + center
            + Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized * offset.x
            + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(_camera.right, Vector3.up).normalized * offset.z;

        //transform.position = charController.center + offset;
/*        transform.position = target.position
            + Vector3.up * offset.y
            + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized * offset.x
            + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized * offset.z;*/

/*        transform.position = new Vector3(
            target.position.x + Vector3.ProjectOnPlane(target.right, Vector3.up).normalized.x * offset.x,
            offset.y,
            target.position.z + Vector3.ProjectOnPlane(target.forward, Vector3.up).normalized.z * offset.z);*/

        transform.eulerAngles = new Vector3(0,target.eulerAngles.y,0);
    }
}
