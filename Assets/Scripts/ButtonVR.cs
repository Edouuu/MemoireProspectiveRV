using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] public UnityEvent onPress;
    [SerializeField] public UnityEvent onRelease;

    private AudioSource sound;
    private bool isPressed;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && (other.gameObject.name == "LeftHand" || other.gameObject.name == "RightHand"))
        {
            button.transform.localPosition = new Vector3(0.006899993f, 0.115f, -0.02119998f);
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "LeftHand" || other.gameObject.name == "RightHand")
        {
            button.transform.localPosition = new Vector3(0.006899993f, 0.1393999f, -0.02119998f);
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
