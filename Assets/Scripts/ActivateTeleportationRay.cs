using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject rightTeleportation;
    [SerializeField] private InputActionProperty joystick;
    [SerializeField] private InputActionProperty rightActivate;

    void Update()
    {
        rightTeleportation.SetActive(joystick.action.ReadValue<Vector2>().y > 0.1f
            || rightActivate.action.ReadValue<float>() >= 0.1f);
    }
}
