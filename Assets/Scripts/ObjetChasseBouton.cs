using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetChasseBouton : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;
    [SerializeField] JeuChasse jeu;

    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void ButtonPressed()
    {
        StartCoroutine(End());
    }
    private IEnumerator End()
    {
        jeu.AjoutPoint();
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(soundEffect);
        yield return new WaitForSeconds(soundEffect.length);
        this.gameObject.SetActive(false);
    }

}
