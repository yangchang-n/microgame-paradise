using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    Text textGameObject;
    Color gameOverTextColor;
    Color winTextColor;
    float resetTimer = 0f;

    void Start()
    {
        textGameObject = GetComponent<Text>();
        gameOverTextColor = new Color(0.5f, 0, 0, 0);
        winTextColor = new Color(0, 0, 0.5f, 0);
        resetTimer = 0f;
    }
    
    void Update()
    {
        if (gameObject.name == "WinText" && GameManager.instance.GetIsCleared())
        {
            if (winTextColor.a < 1f) winTextColor.a += 0.01f;
            GetComponent<Text>().color = winTextColor;

            if (resetTimer < 3f) resetTimer += 0.01f;
            else
            {
                resetTimer = 0f;
                GameManager.instance.ResetAll();
                SceneManager.LoadScene("TitleScene");
            }
        }

        if (gameObject.name == "GameOverText" && GameManager.instance.GetIsGameOver())
        {
            if (gameOverTextColor.a < 1f) gameOverTextColor.a += 0.01f;
            GetComponent<Text>().color = gameOverTextColor;

            if (resetTimer < 3f) resetTimer += 0.01f;
            else
            {
                resetTimer = 0f;
                GameManager.instance.ResetAll();
                SceneManager.LoadScene("TitleScene");
            }
        }
    }

    public void UpdateScoreUI()
    {
        float tempScore = GameManager.instance.GetScore();
        string scoreText = "Score : " + tempScore.ToString("0000");
        textGameObject.text = scoreText;
    }

    public void UpdateHPUI()
    {
        int tempHP = GameObject.Find("Player").GetComponent<PlayerController>().GetHP();
        string hPText = "HP : " + tempHP.ToString("0");
        textGameObject.text = hPText;
    }

    public void InitWinUI()
    {
        GetComponent<Text>().color = new Color(0, 0, 0.5f, 0);
    }

    public void InitGameOverUI()
    {
        GetComponent<Text>().color = new Color(0.5f, 0, 0, 0);
    }
}
