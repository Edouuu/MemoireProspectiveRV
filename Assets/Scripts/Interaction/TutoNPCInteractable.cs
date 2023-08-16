using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TutoNPCInteractable : MonoBehaviour, IInteractable {
    [Header("Other")]
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private GameObject XROrigin;
    [SerializeField] private bool premierTuto;

    [Header("AudioClip")]
    [SerializeField] private AudioClip audioEnter;
    [SerializeField] private AudioClip audioYes;
    [SerializeField] private AudioClip audioNo;
    [SerializeField] private AudioClip audioExit;
    [SerializeField] private AudioClip audioMicActive;

    [Header("Canva")]
    [SerializeField] private TextMeshProUGUI textCanva;
    [SerializeField] private string interactText;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    private Animator animator;
    private AudioSource source;
    private string sentenceRecognized;

    private bool recognition = false;

    private bool tutoFini = false;
    private bool stopCoroutine = false;
    
    

    private void Awake() {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }


    public IEnumerator Interact(int i)
    {
        stopCoroutine = false;

        if (!premierTuto)
        {
            source.PlayOneShot(audioEnter); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioEnter.length);
            playerInteract.NotInteractionState();
            yield break;
        }
        if (i == 1)
        {
            source.PlayOneShot(audioNo); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioNo.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            FirstRecognition(sentenceRecognized);
            recognition = false;
        }
        if (source != null && i == 0 && tutoFini == false)
        {
            source.PlayOneShot(audioEnter); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioEnter.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            FirstRecognition(sentenceRecognized);
            recognition = false;
        }
        if (source != null && i == 0 && tutoFini == true)
        {
            source.PlayOneShot(audioYes); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioYes.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            SecondRecognition(sentenceRecognized);
            recognition = false;
        }
    }

    private void FirstRecognition(string text)
    {
        if (text.ToLower().Contains("oui")
            || text.ToLower().Contains("compris")
            || text.ToLower().Contains("d'accord")
            || text.ToLower().Contains("entendu"))
        {
            tutoFini = true;
            StartCoroutine(AudioPositive());
            return;
        }
        StartCoroutine(Interact(1));
    }

    private IEnumerator AudioPositive()
    {
        if (source != null)
        {
            source.PlayOneShot(audioYes); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioYes.length); // Wait for the audio clip length
            if (premierTuto)
            {
                XROrigin.transform.position = new Vector3(100, 0, 2.5f);
                playerInteract.NotInteractionState();
                yield break;
            }
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            SecondRecognition(sentenceRecognized);
            recognition = false;
        }
    }

    private void SecondRecognition(string text)
    {
        if (text.ToLower().Contains("c'est parti"))
        {
            SceneManager.LoadScene(1);
            return;
        }
        StartCoroutine(Exit());
    }
    private IEnumerator Exit()
    {
        if (source != null)
        {
            source.PlayOneShot(audioExit); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioExit.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            recognition = false;
            playerInteract.NotInteractionState();
        }
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