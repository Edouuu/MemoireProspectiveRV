using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetChasse : MonoBehaviour
{
    [SerializeField] GameObject objetRed;
    [SerializeField] GameObject objetGreen;
    [SerializeField] AudioClip soundEffect;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Phone" && objetGreen.activeSelf == false)
        {
            source.PlayOneShot(soundEffect);
            objetGreen.SetActive(true);
            objetRed.SetActive(false);
        }
    }
}
