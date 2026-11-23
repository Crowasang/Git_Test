using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UI_Title_Button_GameStart : MonoBehaviour
{
    private Button _button = null;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void SetEvent(UnityAction action)
    {
        _button.onClick.AddListener(action);
    }
}
