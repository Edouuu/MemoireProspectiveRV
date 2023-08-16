using Oculus.Voice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;
using static Unity.VisualScripting.Member;

public class CommandeVocale : MonoBehaviour
{
    [SerializeField] private string[] collisionName;
    [SerializeField] private TextMeshProUGUI fullText;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    [Header("AudioClip")]
    [SerializeField] private AudioClip audioBip;

    private AudioSource source;
    
    private string phraseReconnue;
    private bool stopWaitCoroutine = false;
    private bool interactState = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();

        appVoiceExperience.VoiceEvents.onFullTranscription.AddListener((transcription) =>
        {
            transcription = phraseReconnue;
            stopWaitCoroutine = true;
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactState == false && collisionName.Contains(other.name))
        {
            interactState = true;
            StartCoroutine(Interact());
        }
    }

    private IEnumerator Interact()
    {
        source.PlayOneShot(audioBip);
        appVoiceExperience.Activate();
        yield return new WaitUntil(() => stopWaitCoroutine);
        stopWaitCoroutine = false;
        fullText.text = fullText.text + phraseReconnue;
        interactState = false;
    }
}
