using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PhoneInteractable : MonoBehaviour, IInteractable {


    [Header("Other")]
    [SerializeField] private string interactText;
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private GameObject objectToActive;

    [Header("AudioClip")]
    [SerializeField] private AudioClip audioMicActive;

    [Header("Canva")]
    [SerializeField] private TextMeshProUGUI textCanva;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;


    private AudioSource source;
    private string sentenceRecognized;

    private bool recognition = false;



    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    public IEnumerator Interact(int i)
    {
        if (source != null)
        {
            source.PlayOneShot(audioMicActive);
            appVoiceExperience.Activate();
            yield return new WaitUntil(() => recognition);
            recognition = false;
            if (textCanva.text.Length > 300)
            {
                textCanva.text = "";
            }
            textCanva.text += System.DateTime.Now.ToString("HH:mm") + " : " + sentenceRecognized + "\n";
            playerInteract.NotInteractionState();
        }
    }

/*    private IEnumerator Exit()
    {
        textCanva.text = null;
        playerInteract.NotInteractionState();
    }*/

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
        source.Stop();
        recognition = false;
    }
}