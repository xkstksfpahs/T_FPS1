using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainText : MonoBehaviour
{
    public Text mainText;
    //Color textColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainText.color.a > 0)
        StartCoroutine(FadeTextToZero());
    }


    public IEnumerator FadeTextToZero()
    {
        //mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
        yield return new WaitForSeconds(1.5f);
        while (mainText.color.a > 0.0f)
        {
            mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, mainText.color.a - (Time.deltaTime / 50.0f));
            yield return null;
        }
    }
}
