using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionEnterAction : MonoBehaviour
{
    [SerializeField] public UnityEvent Event;
    [SerializeField] private string[] collisionName;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (collisionName.Contains(other.name))
        {
            Event.Invoke();
        }
    }
}
