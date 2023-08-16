using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendeurAudioLoader : MonoBehaviour
{
    [SerializeField] private NPCInteractable npc;
    [SerializeField] private string folderName;
    void Awake()
    {
        int i = 0;
        npc = GetComponent<NPCInteractable>();
        List<Dictionary<string, object>> data = CSVReader.Read("testNPC");
        AudioClip audioClip = Resources.Load(folderName + "/" + i.ToString()) as AudioClip;
        while (audioClip != null)
        {
            npc.AjoutAudioEnter(audioClip);
            i += 1;
            audioClip = Resources.Load(i.ToString()) as AudioClip;
        }
    }
}
