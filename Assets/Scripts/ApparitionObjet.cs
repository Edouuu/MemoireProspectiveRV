using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApparitionObjet : MonoBehaviour
{
    [System.Serializable]
    private struct SlotChasse
    {
        public float time_minutes_apparition;
        public float time_secondes_apparition;
        public float time_minutes_temps;
        public float time_secondes_temps;
        public GameObject Slot;
    }

    [SerializeField] private SlotChasse[] slotChasses;

    private void Start()
    {
        foreach (SlotChasse slotChasse in slotChasses)
        {
            StartCoroutine(StartApparition(slotChasse));
        }
    }

    private IEnumerator StartApparition(SlotChasse slotChasse)
    {
        yield return new WaitForSeconds(slotChasse.time_minutes_apparition * 60 + slotChasse.time_secondes_apparition);
        slotChasse.Slot.SetActive(true);
        yield return new WaitForSeconds(slotChasse.time_minutes_temps * 60 + slotChasse.time_secondes_temps);
        slotChasse.Slot.SetActive(false);
    }

}
