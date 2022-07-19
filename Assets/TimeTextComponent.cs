using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeTextComponent : MonoBehaviour
{
    Text text;
    float baseColor = 0.1960f;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textColorGreen();
        textColorRed();
        if (Input.GetMouseButtonDown(0))
        {
            SetColor(1);
        }
    }


    void textColorGreen()
    {
        if (text.color.g > baseColor)
            text.color = new Color(baseColor, text.color.g - (Time.deltaTime/50), baseColor);
        else if(text.color.g <= baseColor)
            text.color = new Color(baseColor, baseColor, baseColor);
    }

    void textColorRed()
    {
        if (text.color.r > baseColor)
            text.color = new Color(text.color.r - Time.deltaTime, baseColor, baseColor);
        else
            text.color = new Color(baseColor, baseColor, baseColor);
    }

    public void SetColor(int num)
    {
        if (num == 1)
        {
            text.color = new Color(baseColor, 1, baseColor);
        }
        else if(num == 2)
        {
            text.color = new Color(1, baseColor, baseColor);
        }
    }
}
