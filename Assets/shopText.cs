using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shopText : MonoBehaviour
{
    TextMesh text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        //text.color = new Color(255, 255, 255, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (text.color.a < 255)
        {
            //StartCoroutine("FadeToOne");
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 50.0f));
            Debug.Log("업데이트");
        }
    }

    //IEnumerator FadeToOne()
    //{
    //    yield return new WaitForSeconds(1.5f);
    //    while (text.color.a < 255)
    //    {
    //        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 50.0f));
    //        yield return null;
    //    }
    //    Debug.Log("123");
    //}
    void FadeToOne()
    {
        while (text.color.a < 255)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / 50.0f));
            //yield return null;
        }
    }
}
