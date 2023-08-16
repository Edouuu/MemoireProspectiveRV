using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDeplacement : MonoBehaviour
{

    [SerializeField] private Renderer rendererSphere;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "XR Origin")
        {
            rendererSphere.material.color = Color.green;
            this.gameObject.SetActive(false);
        }
    }
}
