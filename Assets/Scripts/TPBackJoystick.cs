using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class TPBackJoystick : MonoBehaviour
{
    [SerializeField] private InputActionReference inputJoystick;
    [SerializeField] private GameObject _camera;
    [SerializeField] [Range(0.1f, 3)] private float distanceTeleportation = 0.5f;
    private bool possible = true;

    void Update()
    {
        var cardinal = CardinalUtility.GetNearestCardinal(inputJoystick.action.ReadValue<Vector2>());
        if  (possible && cardinal == Cardinal.South)
        {
            possible = false;
            Vector3 cameraLook = Camera.main.transform.forward;
            cameraLook.y = 0;
            cameraLook = cameraLook.normalized;
            transform.position -= cameraLook * distanceTeleportation;
        }
        if (cardinal != Cardinal.South)
        {
            possible = true;
        }

    }
}
