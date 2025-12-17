using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    float score = 0;
    bool isCleared = false;
    bool isGameOver = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Time.fixedDeltaTime = 1f / 60f;
        Time.maximumDeltaTime = 0.5f;
    }

    void Start()
    {
        ResetAll();
        GameObject.Find("ScoreText").GetComponent<StageUI>().UpdateScoreUI();
        GameObject.Find("WinText").GetComponent<StageUI>().InitWinUI();
        GameObject.Find("GameOverText").GetComponent<StageUI>().InitGameOverUI();
        GameObject.Find("Player").GetComponent<PlayerController>().SetHP(5);
        GameObject.Find("HPText").GetComponent<StageUI>().UpdateHPUI();
    }

    void Update()
    {
        if (score >= 1000) SetIsCleared(true);
    }

    public void ResetAll()
    {
        score = 0;
        isCleared = false;
        isGameOver = false;
    }

    public void AddScore()
    {
        score += 100;
        GameObject.Find("ScoreText").GetComponent<StageUI>().UpdateScoreUI();
    }

    public float GetScore()
    {
        return score;
    }

    public void SetIsCleared(bool cleared)
    {
        isCleared = cleared;
    }

    public bool GetIsCleared()
    {
        return isCleared;
    }

    public void SetIsGameOver(bool isOver)
    {
        isGameOver = isOver;
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }
}
