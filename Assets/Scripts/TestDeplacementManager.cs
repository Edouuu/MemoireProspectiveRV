using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Unity.VisualScripting.Member;

public class TestDeplacementManager : MonoBehaviour
{
    [SerializeField] GameObject m_gameObject1;
    [SerializeField] GameObject m_gameObject2;
    [SerializeField] GameObject m_gameObject3;
    [SerializeField] GameObject m_gameObject4;

    [SerializeField] public UnityEvent onEnd;

    private AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (m_gameObject1.activeSelf == false
            && m_gameObject2.activeSelf == false
            && m_gameObject3.activeSelf == false
            && m_gameObject4.activeSelf == false)
        {
            sound.Play();
            onEnd.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
