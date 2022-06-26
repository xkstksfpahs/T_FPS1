using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCount : MonoBehaviour
{
    TextMesh text;
    SpawnManager sm;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMesh>();
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        OutText();
    }

    void OutText()
    {
        text.text = sm.count.ToString() + "/" + sm.maxBoxCount.ToString();
        if (sm.count >= 15f && sm.count < 20)
        {
            text.color = new Color(236, 144, 0);
        }
        else if (sm.count >= 20)
        {
            text.color = new Color(255, 0, 0);
        }
        else
            text.color = new Color(255, 255, 255);
    }
}
