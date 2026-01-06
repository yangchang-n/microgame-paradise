using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsUI : MonoBehaviour
{
    public SceneChanger sceneChanger;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sceneChanger.SceneChange("TitleScene");
        }
    }
}
