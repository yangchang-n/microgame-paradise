using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Text textComponent;
    Color gameOverTextColor;
    Color clearTextColor;
    float resetTimer = 0f;

    public GameObject player;

    void Start()
    {
        textComponent = GetComponent<Text>();
        gameOverTextColor = new Color(0.5f, 0, 0, 0);
        clearTextColor = new Color(0, 0, 0.5f, 0);
        resetTimer = 0f;
    }

    private void FixedUpdate()
    {
        if (gameObject.name == "ClearText" && GameManager.instance.GetGameClear())
        {
            if (clearTextColor.a < 1f) clearTextColor.a += 0.01f;
            textComponent.color = clearTextColor;

            if (resetTimer < 3f) resetTimer += Time.deltaTime;
            else
            {
                resetTimer = 0f;
                GameManager.instance.ResetAll();
                SceneManager.LoadScene("Title");
            }
        }

        if (gameObject.name == "GameOverText" && GameManager.instance.GetGameOver())
        {
            if (gameOverTextColor.a < 1f) gameOverTextColor.a += 0.01f;
            textComponent.color = gameOverTextColor;

            if (resetTimer < 3f) resetTimer += Time.deltaTime;
            else
            {
                resetTimer = 0f;
                GameManager.instance.ResetAll();
                SceneManager.LoadScene("Title");
            }
        }
    }

    public void UpdateScoreUI()
    {
        float tempScore = GameManager.instance.GetScore();
        string scoreText = "Score : " + tempScore.ToString("000");
        textComponent.text = scoreText;
    }

    public void UpdateHPUI()
    {
        int tempHP = player.GetComponent<PlayerController>().GetHP();
        string hPText = "HP : " + tempHP.ToString("0");
        textComponent.text = hPText;
    }
}
