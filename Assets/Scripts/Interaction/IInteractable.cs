using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable {
    IEnumerator Interact(int i);
    string GetInteractText();
    Transform GetTransform();

    void StopDistance();

    void SetRecognizedSentence(string sentence);
    void SetRecognitionTrue();
}