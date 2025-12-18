using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    float moveSpeed = 0.01f;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        Time.fixedDeltaTime = 1f / 60f;
        Time.maximumDeltaTime = 0.5f;
    }

    void Update()
    {
        transform.Translate(0, -moveSpeed, 0);
        if (transform.position.y < -13.5) transform.Translate(0, 27, 0);
    }
}
