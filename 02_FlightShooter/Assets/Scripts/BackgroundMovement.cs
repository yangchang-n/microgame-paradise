using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    float moveSpeed = 0.008f;

    void Update()
    {
        transform.Translate(0, -moveSpeed, 0);
        if (transform.position.y < -10.2f) transform.Translate(0, 20.4f, 0);
    }
}
