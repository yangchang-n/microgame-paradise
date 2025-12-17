using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGlitter : MonoBehaviour
{
    Color color;
    bool alphaUp;
    float glittingAmount;

    void Start()
    {
        color = new Color(1, 1, 1, 0);
        alphaUp = true;
        glittingAmount = 0.02f;
    }

    void Update()
    {
        if (alphaUp)
        {
            color.a += glittingAmount;
            if (color.a >= 1.0f) alphaUp = false;
        }
        else
        {
            color.a -= glittingAmount;
            if (color.a <= 0.0f) alphaUp = true;
        }

        GetComponent<Text>().color = color;
    }
}
