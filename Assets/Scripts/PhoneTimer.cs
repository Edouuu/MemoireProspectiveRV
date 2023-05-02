using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneTimer : MonoBehaviour
{
    [SerializeField] private InputActionProperty rightGrabActivate;

    public Camera cameraToCheck;
    public GameObject objectToCheck;
    public GameObject objectToShow;
    public GameObject objectToHide;

    private bool objectIsActive;

    private void Start()
    {
        objectIsActive = objectToShow.activeSelf;
    }

    private void Update()
    {
        if (cameraToCheck == null || objectToCheck == null)
            return;

        // Get the position of the object in world space
        Vector3 objectPosition = objectToCheck.transform.position;

        // Convert the object's world space position to a viewport position
        Vector3 viewportPosition = cameraToCheck.WorldToViewportPoint(objectPosition);

        // Check if the object is currently visible on screen
        bool objectIsVisible = (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1 && viewportPosition.z > 0);
        bool objectRightLowCorner = (viewportPosition.x > 0.5f && viewportPosition.y < 0.0f);

        // Case 1 : you grab when object isn't in your FOV
        if (objectRightLowCorner && !objectIsVisible && !objectIsActive && rightGrabActivate.action.ReadValue<float>() > 0.1f)
        {
            objectToShow.SetActive(true);
            objectToHide.SetActive(false);
            objectIsActive = true;

        }
        if (!objectIsVisible && objectIsActive && rightGrabActivate.action.ReadValue<float>() == 0f)
        {
            objectToShow.SetActive(false);
            objectToHide.SetActive(true);
            objectIsActive = false;
        }
    }
}
