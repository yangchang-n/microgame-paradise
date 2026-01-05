using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHammer : MonoBehaviour
{
    float rotationSpeed;

    private void Start()
    {
        rotationSpeed = 1f;
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
