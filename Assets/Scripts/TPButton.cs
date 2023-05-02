using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPButton : MonoBehaviour
{
    public InputActionReference buttonAInput;
    public GameObject _camera;
    public GameObject _XROrigin;
    [Range(0.5f, 10f)] public float distanceTeleportation;

    // Update is called once per frame
    void Update()
    {
        if (buttonAInput.action.triggered)
        {
            Debug.Log("Bouton A actif");
            Vector3 cameraLook = Camera.main.transform.forward;
            cameraLook.y = 0;
            _XROrigin.transform.position += cameraLook * distanceTeleportation; 
        }
       

    }
}
