using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FirstNPCInteractable : MonoBehaviour, IInteractable {

    [System.Serializable]
    private struct Interaction
    {
        public string wordToFind;
        public AudioClip audioClipIfReussite;
        public string marketToFind;
    }

    [Header("Other")]
    [SerializeField] private string interactText;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private GameObject objectToActive;

    [Header("AudioClip")]
    [SerializeField] private AudioClip audioEnter;
    [SerializeField] private AudioClip audioRetry;
    [SerializeField] private AudioClip audioExit;
    [SerializeField] private AudioClip audioFindSomething;
    [SerializeField] private AudioClip audioMicActive;
    [SerializeField] private AudioClip audioReussiteDirectionCentre;

    [Header("Canva")]
    [SerializeField] private TextMeshProUGUI textCanva;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    [Header("Interactions")]
    [SerializeField] private Interaction[] interactionList;

    private Animator animator;
    private AudioSource source;
    private string sentenceRecognized;

    private bool recognition = false;

    private bool intention0 = false;
    private bool intention1 = false;
    private bool intention2 = false;

    private int numberFound = 0;
    private bool stopCoroutine = false;



    private void Awake() {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        GameObject playerInteractObject = GameObject.FindGameObjectWithTag("Player");
        playerInteract = playerInteractObject.GetComponent<PlayerInteract>();
    }

    public IEnumerator Interact(int i)
    {
        if (source != null)
        {
            stopCoroutine = false;
            intention0 = false;
            intention1 = false;
            intention2 = false;
            source.PlayOneShot(audioEnter); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioEnter.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            recognition = false;
            WordRecognition(sentenceRecognized);
        }
    }

    private void WordRecognition(string text)
    {
        if (text == null)
        {
            StartCoroutine(Exit());
            return;
        }
        foreach (Interaction interaction in interactionList)
        {
            if (text.ToLower().Contains(interaction.wordToFind) && text.ToLower().Contains(interaction.marketToFind))
            {
                StartCoroutine(MiseAJourIntention(interaction));
                return;
            }
        }
        StartCoroutine(Reessayer());
    }

    private IEnumerator MiseAJourIntention(Interaction interaction)
    {
        if (!intention0 && interaction.wordToFind == interactionList[0].wordToFind)
        {
            intention0 = true;
            numberFound += 1;
        }
        if (!intention1 && interaction.wordToFind == interactionList[1].wordToFind)
        {
            intention1 = true;
            numberFound += 1;
        }
        if (!intention2 && interaction.wordToFind == interactionList[2].wordToFind)
        {
            intention2 = true;
            numberFound += 1;
        }

        // Cas où tout a été dit
        if (intention0 && intention1 && intention2)
        {
            source.PlayOneShot(audioReussiteDirectionCentre);
            StartCoroutine(TalkAnimation());
            textCanva.text = null;
            objectToActive.SetActive(true);
            playerInteract.NotInteractionState();
        }
        // Cas où il reste des intentions à donner
        else
        {
            source.PlayOneShot(interaction.audioClipIfReussite); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(interaction.audioClipIfReussite.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            //yield return new WaitForSeconds(5.0f);
            yield return new WaitUntil(() => recognition);
            WordRecognition(sentenceRecognized);
            recognition = false;
        }
    }

    private IEnumerator Reessayer()
    {
        source.PlayOneShot(audioRetry); // Start playing the audio
        StartCoroutine(TalkAnimation());
        yield return new WaitForSeconds(audioRetry.length); // Wait for the audio clip length
        if (stopCoroutine) { yield break; }
        source.PlayOneShot(audioMicActive);
        appVoiceExperience.Activate();
        yield return new WaitUntil(() => recognition);
        recognition = false;
        WordRecognition(sentenceRecognized);
    }

    private IEnumerator Exit()
    {
        source.PlayOneShot(audioExit); // Start playing the audio
        StartCoroutine(TalkAnimation());
        textCanva.text = null;
        yield return new WaitForSeconds(audioExit.length); // Wait for the audio clip length
        playerInteract.NotInteractionState();
    }

    private IEnumerator TalkAnimation()
    {
        animator.SetBool("Talk", true);
        yield return new WaitForSeconds(3.0f);
        animator.SetBool("Talk", false);
        yield return null;
    }
    public void SetRecognizedSentence(string sentence)
    {
        sentenceRecognized = sentence;
    }
    public string GetInteractText() {
        return interactText;
    }
    public Transform GetTransform() {
        return transform;
    }

    public void SetRecognitionTrue()
    {
        recognition = true;
    }

    public void StopDistance()
    {
        stopCoroutine = true;
        source.Stop();
        animator.SetBool("Talk", false);
        recognition = false;
    }
}