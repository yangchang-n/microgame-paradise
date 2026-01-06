using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    public SceneChanger sceneChanger;

    public Button startButton;
    public Button creditButton;
    public Button quitButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        creditButton.onClick.AddListener(LoadCredit);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartGame()
    {
        GameManager.instance.ResetAll();
        sceneChanger.SceneChange("Stage");
    }

    public void LoadCredit()
    {
        sceneChanger.SceneChange("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}