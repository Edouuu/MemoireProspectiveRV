using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

public class JeuChasse : MonoBehaviour
{
    //[SerializeField] private UnityEvent Event;
    [SerializeField] private TextMeshProUGUI textTelephoneProgression;

    [Header("AudioClip")]
    [SerializeField] private AudioClip audioSuccess;

    private AudioSource source;
    //private bool updateActive = true;
    private int point;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void AjoutPoint()
    {
        point += 1;
        textTelephoneProgression.text = "Points Jeu concours : " + point.ToString() + "/10";
    }

    /*    void Update()
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
        }*/


}
