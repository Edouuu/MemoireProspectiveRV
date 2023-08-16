using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] private string texte;
    [SerializeField] private TextMeshPro textTMP;
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private Image imageLoader;
    [SerializeField] private GameObject XROrigin;

    [Header("Scene Or Transform")]
    [SerializeField] private int numeroScene;
    [SerializeField] private bool SceneMode = true;
    [SerializeField] private Transform transformTP;

    

    private bool timerBool = false;
    private float timer = 0.0f;


    private void Update()
    {
        if (timerBool)
        {
            timer += Time.deltaTime;
        }

        if (timer >= 5.0f)
        {
            if (SceneMode) { SceneManager.LoadScene(numeroScene); }
            else { XROrigin.transform.position = transformTP.position; }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Enter : " + other);
        if (other.name == "XR Origin")
        {
            timerBool= true;
            loaderCanvas.SetActive(true);
            imageLoader.fillAmount = 0.0f;
            textTMP.text = texte;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "XR Origin")
        {
            loaderCanvas.SetActive(false);
            timerBool = false;
            timer = 0.0f;
        }
    }
}
