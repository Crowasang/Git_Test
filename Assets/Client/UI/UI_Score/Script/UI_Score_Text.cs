using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score_Text : MonoBehaviour
{
    private Text _text = null;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }
}
