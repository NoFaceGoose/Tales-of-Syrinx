using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconController : MonoBehaviour
{
    public Color color1 = new Color(0,102,204, 1f);
    public Color color2 = new Color(255,255,0, 1f);
    float timeCounter = 0.0f;
    bool colorFlag = true;
    public GameObject cube;
    Renderer cubeRenderer;


    void Start()
    {
        cubeRenderer = cube.GetComponent<Renderer>();
    }

    void Update()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > 1)
        {
            changeColor();
            timeCounter = 0;
        }
    }

    void changeColor()
    {
        if(colorFlag)
        {
            cubeRenderer.material.SetColor("_Color", color1);
            colorFlag = !colorFlag;
        }
        else
        {
            cubeRenderer.material.SetColor("_Color", color2);
            colorFlag = !colorFlag;
        }
    }
}
