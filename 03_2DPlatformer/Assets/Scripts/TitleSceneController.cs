using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.ResetAll();
            SceneManager.LoadScene("Stage1");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SceneManager.LoadScene("Credit");
        }
    }
}
