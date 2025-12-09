using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    float moveSpeed = 0.005f;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(0, -moveSpeed, 0);
        if (transform.position.y < -13.5) transform.Translate(0, 27, 0);
    }
}
