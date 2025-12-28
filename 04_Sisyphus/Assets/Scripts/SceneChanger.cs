using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Image fadeCoverImage;
    float fadeDuration;
    bool fadeInDone;

    private void Start()
    {
        fadeCoverImage = GameObject.Find("FadeCoverImage").GetComponent<Image>();
        Color color = fadeCoverImage.color;
        color.a = 1f;
        fadeCoverImage.color = color;
        fadeDuration = 1f;
        fadeInDone = false;
    }

    private void Update()
    {
        if (!fadeInDone)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneOpen(currentSceneName);
            fadeInDone = true;
        }
    }

    public void SceneOpen(string sceneName)
    {
        StartCoroutine(FadeIn(sceneName));
    }

    public void SceneChange(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private void ShowCursor(string sceneName)
    {
        bool showCursor = (sceneName == "TitleScene");
        Cursor.visible = showCursor;
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }

    private IEnumerator FadeIn(string sceneName)
    {
        ShowCursor(sceneName);
        float fadeElapsedTime = 0f;
        while (fadeElapsedTime < fadeDuration)
        {
            fadeElapsedTime += Time.deltaTime;
            SetFadeAlpha(1f - (fadeElapsedTime / fadeDuration));
            yield return null;
        }
        SetFadeAlpha(0f);
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        float fadeElapsedTime = 0f;
        while (fadeElapsedTime < fadeDuration)
        {
            fadeElapsedTime += Time.deltaTime;
            SetFadeAlpha(fadeElapsedTime / fadeDuration);
            yield return null;
        }
        SetFadeAlpha(1f);
        SceneManager.LoadScene(sceneName);
    }

    private void SetFadeAlpha(float alpha)
    {
        Color color = fadeCoverImage.color;
        color.a = alpha;
        fadeCoverImage.color = color;
    }
}