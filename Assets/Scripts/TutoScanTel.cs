using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutoScanTel : MonoBehaviour
{
    [SerializeField] private GameObject redLight;
    [SerializeField] public UnityEvent onEnd;

    void Update()
    {
        if (redLight.activeSelf == false)
        {
            StartCoroutine(Exit());
        }
    }

    private IEnumerator Exit()
    {
        yield return new WaitForSeconds(1.0f);
        onEnd.Invoke();
        this.gameObject.SetActive(false);
    }
}
