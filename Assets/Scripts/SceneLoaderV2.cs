using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoaderV2 : MonoBehaviour
{
    [SerializeField] private int numeroScene;

    public void SceneLoader()
    {
        SceneManager.LoadScene(numeroScene);
    }
}
