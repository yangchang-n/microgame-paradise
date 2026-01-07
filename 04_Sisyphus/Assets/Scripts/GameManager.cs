using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int windowedWidth;
    int windowedHeight;

    public Vector3 rockRespawnPoint = new Vector3(0, 6f, 4f);
    public Vector3 playerRespawnPoint = new Vector3(0, 2f, 0);

    bool isCleared;
    bool isGameOver;
    public bool isPaused;
    public bool isInStage;
    public float stageElapsedTime;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        windowedWidth = 1280;
        windowedHeight = 720;

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void Start()
    {
        ResetAll();
        rockRespawnPoint = new Vector3(0, 6f, 4f);
        playerRespawnPoint = new Vector3(0, 2f, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) ToggleFullscreen();
        if (isInStage && !isPaused && !isCleared) stageElapsedTime += Time.deltaTime;
    }

    public void ResetAll()
    {
        isCleared = false;
        isGameOver = false;
        isPaused = false;
        isInStage = false;
        stageElapsedTime = 0;
    }

    public void SetCleared(bool clear)
    {
        Debug.Log("게임 클리어");
        isCleared = clear;
    }

    public bool GetCleared()
    {
        return isCleared;
    }

    public void SetGameOver(bool over)
    {
        isGameOver = over;
    }

    public bool GetGameOver()
    {
        return isGameOver;
    }

    void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(windowedWidth, windowedHeight, FullScreenMode.Windowed);
        }
        else
        {
            Resolution monitorRes = Screen.currentResolution;
            Screen.SetResolution(monitorRes.width, monitorRes.height, FullScreenMode.FullScreenWindow);
        }
    }
}
