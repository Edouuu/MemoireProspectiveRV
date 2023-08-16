using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WatchTimer : MonoBehaviour
{
    public TextMeshProUGUI _textMeshPro;

    private void Start()
    {
        _textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _textMeshPro.text = System.DateTime.Now.ToString("HH:mm:ss");
    }


}
