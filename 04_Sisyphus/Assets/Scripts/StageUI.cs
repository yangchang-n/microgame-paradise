using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour
{
    public SceneChanger sceneChanger;
    public GameObject pausePanel;
    public GameObject clearPanel;
    public Button resumeButton;
    public Button titleButton;
    public TMP_Text timeText;

    private void Start()
    {
        GameManager.instance.isInStage = true;
        GameManager.instance.isPaused = false;

        pausePanel.SetActive(false);
        clearPanel.SetActive(false);

        resumeButton.onClick.AddListener(ResumeGame);
        titleButton.onClick.AddListener(ReturnToTitle);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.instance.GetCleared())
        {
            if (!GameManager.instance.isPaused) PauseGame();
            else ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        timeText.text = TimeFormat(GameManager.instance.stageElapsedTime);
        pausePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GameManager.instance.isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.instance.isPaused = false;
    }

    void ReturnToTitle()
    {
        Time.timeScale = 1;
        GameManager.instance.ResetAll();
        sceneChanger.SceneChange("TitleScene");
    }

    string TimeFormat(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00} m {1:00} s", minutes, seconds);
    }
}
