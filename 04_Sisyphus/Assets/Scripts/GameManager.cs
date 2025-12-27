using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Vector3 rockRespawnPoint = new Vector3(0, 6f, 4f);
    public Vector3 playerRespawnPoint = new Vector3(0, 2f, 0);

    bool isCleared;
    bool isGameOver;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void Start()
    {
        ResetAll();
        rockRespawnPoint = new Vector3(0, 6f, 4f);
        playerRespawnPoint = new Vector3(0, 2f, 0);
    }

    public void ResetAll()
    {
        isCleared = false;
        isGameOver = false;
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
}
