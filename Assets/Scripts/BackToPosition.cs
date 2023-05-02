using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BackToPosition : MonoBehaviour
{
    [SerializeField] private InputActionProperty rightGrabActivate;
    [SerializeField] private Transform XRTransform;
    private Vector3 m_Position;
    // Start is called before the first frame update
    void Start()
    {
        m_Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (rightGrabActivate.action.ReadValue<float>() == 0f)
        {
            transform.position = m_Position + XRTransform.position;
        }
    }
}
