using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnoncesManager : MonoBehaviour
{
    [System.Serializable]
    private struct Annonce
    {
        public float time_minutes;
        public float time_secondes;
        public AudioClip audioClipAnnonce;
    }

    [SerializeField] private Annonce[] annonces;
    [SerializeField] private AudioClip jingle;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }

    private void Start()
    {
        foreach (Annonce annonce in annonces)
        {
            StartCoroutine(StartAnnonce(annonce));
        }
    }

    private IEnumerator StartAnnonce(Annonce annonce)
    {
        yield return new WaitForSeconds(annonce.time_minutes * 60 + annonce.time_secondes);
        audioSource.PlayOneShot(jingle);
        yield return new WaitForSeconds(jingle.length);
        audioSource.PlayOneShot(annonce.audioClipAnnonce);
        
    }
}
