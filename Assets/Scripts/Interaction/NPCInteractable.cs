using Oculus.Voice;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCInteractable : MonoBehaviour, IInteractable {

    [System.Serializable]
    private struct Interaction
    {
        public string wordToFind;
        public AudioClip audioClipIfReussite;
        //public string[] verbToFindList;
        public GameObject prefabToInvoke;
    }

    [Header("Other")]
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private Transform transformObjPos;

    [Header("AudioClip")]
    [SerializeField] private AudioClip[] audioEnter;
    [SerializeField] private AudioClip[] audioExit;
    //[SerializeField] private AudioClip audioFindSomething;
    [SerializeField] private AudioClip audioMicActive;

    [Header("Canva")]
    [SerializeField] private TextMeshProUGUI textCanva;
    [SerializeField] private string interactText;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    [Header("Interactions")]
    [SerializeField] private Interaction[] interactionList;

    private Animator animator;
    private AudioSource source;
    private System.Random random;
    private string sentenceRecognized;

    private bool recognition = false;
    private bool stopCoroutine = false;

    

    private void Awake() {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        if (appVoiceExperience == null) {
            GameObject appVoiceExperienceGameObject = GameObject.FindGameObjectWithTag("AppVoiceExperience");
            appVoiceExperience = appVoiceExperienceGameObject.GetComponent<AppVoiceExperience>();
        }
        
        random = new System.Random();
    }

    public IEnumerator Interact(int i)
    {
        stopCoroutine = false;
        if (source != null)
        {
            AudioClip audioPlayed = audioEnter[random.Next(0, audioEnter.Length)];
            source.PlayOneShot(audioPlayed); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioPlayed.length); // Wait for the audio clip length
            if (stopCoroutine) { yield break; }
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            WordRecognition(sentenceRecognized);
            recognition = false;
        }
    }

    private void WordRecognition(string text)
    {
        if (sentenceRecognized == null)
        {
            StartCoroutine(Exit());
            return;
        }
        foreach (Interaction interaction in interactionList)
        {
            if (text.ToLower().Contains(interaction.wordToFind))
            {
                source.PlayOneShot(interaction.audioClipIfReussite); // Start playing the audio
                StartCoroutine(TalkAnimation());
                GameObject obj = Instantiate(interaction.prefabToInvoke,
                    transformObjPos.position, transformObjPos.rotation);

                if (obj.GetComponentInChildren<TextMeshProUGUI>() != null)
                {
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = interaction.wordToFind;
                }
                textCanva.text = null;
                playerInteract.NotInteractionState();
                return;
            }
        }
        StartCoroutine(Exit());
        return;
    }

/*    private IEnumerator AudioFindSomething(Interaction interaction, string word, string verb)
    {
        // Affichage de ce qui a été demandé
        if (verb!= null)
        {
            textCanva.text = "Objet : " + word + ", Verbe : " + verb + "er";
        }
        else
        {
            textCanva.text = "Objet : " + word + ", Verbe : Non reconnu";
        }
        
        if (source != null)
        {
            source.PlayOneShot(audioFindSomething); // Start playing the audio
            StartCoroutine(TalkAnimation());
            yield return new WaitForSeconds(audioFindSomething.length); // Wait for the audio clip length
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            YesRecognition(interaction);
            recognition = false;
        }
    }*/

/*    private void YesRecognition(Interaction interaction)
    {
        if (sentenceRecognized == null)
        {
            StartCoroutine(Exit());
            return;
        }
        if (sentenceRecognized.ToLower().Contains("oui"))
        {
            source.PlayOneShot(interaction.audioClipIfReussite); // Start playing the audio
            StartCoroutine(TalkAnimation());
            Instantiate(interaction.prefabToInvoke,
                interaction.transformObjPos.position, interaction.transformObjPos.rotation);
            textCanva.text = null;
            playerInteract.NotInteractionState();
            

        }
        else if (sentenceRecognized.ToLower().Contains("non"))
        {
            StartCoroutine(Exit());
        }
        else
        {
            StartCoroutine(Exit());
        }
    }*/

    private IEnumerator Exit()
    {
        AudioClip audioPlayed = audioExit[random.Next(0, audioExit.Length)];
        source.PlayOneShot(audioPlayed); // Start playing the audio
        StartCoroutine(TalkAnimation());
        textCanva.text = null;
        yield return new WaitForSeconds(audioPlayed.length); // Wait for the audio clip length
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
    
    public void AjoutInteraction(string _wordToFind, string _prefabName)
    {
        Interaction inter = new Interaction();
        inter.wordToFind = _wordToFind;
        if (_prefabName == null || _prefabName == "")
        {
            inter.prefabToInvoke = Resources.Load<GameObject>("Prefabs/default");
        }
        else { inter.prefabToInvoke = Resources.Load<GameObject>("Prefabs/" + _prefabName); }

        inter.audioClipIfReussite = Resources.Load<AudioClip>("AudioClip/defaultObjectResponse");

        //inter.verbToFindList = _verbToFindList;

        // Redimensionner le tableau pour ajouter un nouvel élément
        Array.Resize(ref interactionList, interactionList.Length + 1);

        // Ajouter le nouvel élément à la dernière position du tableau
        interactionList[interactionList.Length - 1] = inter;
    }

    public void AjoutAudioEnter(AudioClip audioSource)
    {
        Array.Resize(ref audioEnter, audioEnter.Length + 1);
        audioEnter[audioEnter.Length - 1] = audioSource;
    }
    public void StopDistance()
    {
        stopCoroutine = true;
        source.Stop();
        animator.SetBool("Talk", false);
        recognition = false;
    }
}