using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearPoint : MonoBehaviour
{
    public SceneChanger sceneChanger;
    public ParticleSystem clearParticle;
    public GameObject clearPanel;
    public TMP_Text timeText;

    float afterClearTimer;
    float showClearPanelTime;
    float returnTitleTime;

    private void Start()
    {
        afterClearTimer = 0;
        showClearPanelTime = 1f;
        returnTitleTime = 4f;
    }

    private void Update()
    {
        if (GameManager.instance.GetCleared())
        {
            afterClearTimer += Time.deltaTime;
            if (afterClearTimer > returnTitleTime)
            {
                sceneChanger.SceneChange("TitleScene");
            }
            else if (afterClearTimer > showClearPanelTime)
            {
                timeText.text = TimeFormat(GameManager.instance.stage1ElapsedTime);
                clearPanel.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            Vector3 tempPos = transform.position;
            tempPos.y += 1f;
            Instantiate(clearParticle, tempPos, transform.rotation);
            GameManager.instance.SetCleared(true);
        }
    }

    string TimeFormat(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);
        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;
        return string.Format("{0:00} m {1:00} s", minutes, seconds);
    }
}
