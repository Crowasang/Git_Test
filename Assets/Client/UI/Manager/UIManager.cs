using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private UI_Score _uiScore = null;
    private UI_GameOver _uiGameOver = null;
    private UI_GameClear _uiGameClear = null;

    private void Awake()
    {
        _uiScore = GetComponentInChildren<UI_Score>();
        _uiGameOver = GetComponentInChildren<UI_GameOver>();
        _uiGameClear = GetComponentInChildren<UI_GameClear>();
    }

    public void Init()
    {
        _uiGameClear.gameObject.SetActive(false);
        _uiGameOver.gameObject.SetActive(false);
    }

    public void UpdateScoreUI(int score)
    {
        _uiScore.UpdateScore(score);
    }

    public void ShowGameOverUI()
    {
        _uiGameOver.gameObject.SetActive(true);
    }

    public void ShowGameClearUI()
    {
        _uiGameClear.gameObject.SetActive(true);
    }
}
