using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CSVLoader : MonoBehaviour
{
    [SerializeField] private NPCInteractable npc;
    [SerializeField] private string fileName;
    void Awake()
    {

        List<Dictionary<string, object>> data = CSVReader.Read(fileName);



        for (var i = 0; i < data.Count; i++)
        {
            /*            Debug.Log("name " + data[i]["name"] + " " +
                               "age " + data[i]["age"] + " " +
                               "speed " + data[i]["speed"] + " " +
                               "desc " + data[i]["description"]);*/
            npc.AjoutInteraction(data[i]["word"].ToString(), data[i]["prefab"].ToString());
        }

    }
}