using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CSVWriter : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    private string[] rowDataTemp = new string[3];

    [SerializeField] private string fileName;

    // Use this for initialization
    void Start()
    {

        // Creating First row of titles manually..
        StartLine();
    }

    private void StartLine()
    {
        rowDataTemp[0] = "Time";
        rowDataTemp[1] = "Nom du Vendeur/Téléphone";
        rowDataTemp[2] = "PhrasePrononcee";
        rowData.Add(rowDataTemp);
    }

    public void AddOneLine(string transcription, IInteractable interactable)
    {
        rowDataTemp = new string[3];
        rowDataTemp[0] = System.DateTime.Now.ToString("HH:mm:ss"); // Time
        rowDataTemp[1] = interactable.GetInteractText(); // CHANGER POUR GETNAME
        rowDataTemp[2] = transcription; // Phrase
        rowData.Add(rowDataTemp);
    }

    void OnApplicationQuit()
    {
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath();

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }



    // Following method is used to retrive the relative path as device platform
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + fileName + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+fileName+".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+fileName+".csv";
#else
        return Application.dataPath +"/"+fileName+".csv";
#endif
    }
}
