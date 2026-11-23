using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UI_Title : MonoBehaviour
{
    private UI_Title_Button_GameStart _uiGameStart = null;

    private void Awake()
    {
        _uiGameStart = GetComponentInChildren<UI_Title_Button_GameStart>();
    }

    private void Start()
    {
        SetButtonEvent();
    }

    public void SetButtonEvent()
    {
        _uiGameStart.SetEvent(ChangeScene);
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
