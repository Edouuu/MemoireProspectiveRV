using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

public class JeuChasse : MonoBehaviour
{
    [SerializeField] private GameObject[] listeObjet;
    [SerializeField] private UnityEvent Event;

    [Header("AudioClip")]
    [SerializeField] private AudioClip audioSuccess;

    private AudioSource source;
    private bool updateActive = true;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (updateActive == true)
        {
            int ok = 0;
            foreach (GameObject objet in listeObjet)
            {
                if (objet.activeSelf == true)
                {
                    ok += 1;
                    if (ok == listeObjet.Length)
                    {
                        Event.Invoke();
                    }
                }
            }
        
        }

    }

    public void SoundOn()
    {
        StartCoroutine(End());
    }
    private IEnumerator End()
    {
        yield return new WaitForSeconds(2.0f);
        Debug.Log("Chasse Complétée");
        source.PlayOneShot(audioSuccess);
        yield return new WaitForSeconds(audioSuccess.length);
        this.gameObject.SetActive(false);
    }
}
