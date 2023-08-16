using Oculus.Voice.Demo.LightTraitsDemo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadDebug : MonoBehaviour
{
    [SerializeField] private InputActionReference aButton;
    [SerializeField] private InputActionReference bButton;

    private float time = 0;


    void Update()
    {
        if (aButton.action.ReadValue<float>() >= 0.8f && bButton.action.ReadValue<float>() >= 0.8f)
        {
            time += Time.deltaTime;
            if (time >= 3.0f)
            {
                SceneManager.LoadScene(0);
            }
        }
        else { time = 0f; }
    }
}
