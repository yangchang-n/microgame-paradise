using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    bool isGameCleared;
    bool isGameOver;

    int enemyKilled;
    int score;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }
    void Start()
    {
        ResetAll();
    }

    void Update()
    {
        if (score >= 300 && !isGameOver)
        {
            SetGameClear(true);
        }
    }

    public void ResetAll()
    {
        isGameCleared = false;
        isGameOver = false;
        enemyKilled = 0;
        score = 0;
    }

    public void EnemyKill()
    {
        enemyKilled++;
        score += 100;
        GameObject.Find("ScoreText").GetComponent<UIManager>().UpdateScoreUI();
    }

    public int GetScore()
    {
        return score;
    }

    public void SetGameClear(bool clear)
    {
        isGameCleared = clear;
    }

    public bool GetGameClear()
    {
        return isGameCleared;
    }

    public void SetGameOver(bool gameOver)
    {
        isGameOver = gameOver;
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }
}
