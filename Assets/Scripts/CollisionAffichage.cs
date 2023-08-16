using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAffichage : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Enter : " + other);
        if (other.name == "XR Origin")
        {
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("Collision Exit : " + other);

        if (other.name == "XR Origin")
        {
            objectToActivate.SetActive(false);
        }
    }
}
