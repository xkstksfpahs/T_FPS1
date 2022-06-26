using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour
{
    Player pl;
    bool a = false;
    Text tt;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        tt = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textCcool();
    }

    IEnumerator textCool()
    {
        yield return new WaitForSeconds(0.3f);
        if (pl.killPoint >= 100)
        {
            if (a == false) // a값이 0일때
            {

            }
            else if(a == true) // a값이 255일때
            {

            }
        }
    }
    void textCcool()
    {
        if (pl.killPoint >= 100)
        {
            if (a == false) // a값이 0일때
            {
                tt.color = new Color(tt.color.r, tt.color.g, tt.color.b, tt.color.a + (Time.deltaTime));
                if(tt.color.a >= 1)
                {
                    a = true;
                }
            }
            else if (a == true) // a값이 255일때
            {
                tt.color = new Color(tt.color.r, tt.color.g, tt.color.b, tt.color.a - (Time.deltaTime));
                if (tt.color.a <= 0)
                {
                    a = false;
                }
            }
        }
        else
            tt.color = new Color(tt.color.r, tt.color.g, tt.color.b, 0);
    }
}
