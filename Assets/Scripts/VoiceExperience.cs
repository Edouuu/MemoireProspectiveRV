using Oculus.Voice;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class VoiceExperience : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionProperty inputButton;

    [Header("CSVWriter")]
    [SerializeField] private CSVWriter csvWriter;

    [Header("Voice")]
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI fullTranscriptText;
    [SerializeField] private TextMeshProUGUI partialTranscriptText;

    [SerializeField] private IInteractable interactable;
   

    private void Awake()
    {
        fullTranscriptText.text = partialTranscriptText.text = string.Empty;

        appVoiceExperience.VoiceEvents.onFullTranscription.AddListener((transcription) =>
        {
            //fullTranscriptText.text = "Full Transcription : " + transcription;
            if (interactable != null)
            {
                Debug.Log("Transcription reconnue : " + transcription);
                interactable.SetRecognizedSentence(transcription);
                interactable.SetRecognitionTrue();
                csvWriter.AddOneLine(transcription, interactable);
                appVoiceExperience.Deactivate();
            }
        });        
        
        appVoiceExperience.VoiceEvents.onPartialTranscription.AddListener((transcription) =>
        {
            //partialTranscriptText.text = "Partial Transcription : " + transcription;
        });

        appVoiceExperience.VoiceEvents.OnRequestCreated.AddListener((request) =>
        {
            Debug.Log("OnRequestCreated Active");
        });

        appVoiceExperience.VoiceEvents.OnRequestCompleted.AddListener(() =>
        {
            Debug.Log("OnRequestCompleted Active");
        });
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        /*if (inputButton.action.triggered && !appVoiceActive)
        {
            appVoiceExperience.Activate();
        }*/
    }

    public void SetColor(string[] info)
    {
        DisplayValues("SetColor:", info);
/*        if (info.Length> 0 && ColorUtility.TryParseHtmlString(info[0], out Color color))
        {
            foreach (var controller in controllers)
            {
                controller.SetColor(color);
            }
        }*/
    }

    private static void DisplayValues(string prefix, string[] info)
    {
        foreach (var i in info)
        {
            Debug.Log($"{prefix} {i}");
        }
    }

    public void SetInteractable(IInteractable _interactable)
    {
        interactable = _interactable;
    }
}
