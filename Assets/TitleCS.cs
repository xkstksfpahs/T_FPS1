using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCS : MonoBehaviour
{
    public int[] boardRound = new int[5], boardKill = new int[5];
    int logNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        aa();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void aa()
    {
        if (logNum != PlayerPrefs.GetInt("Log"))
        {
            PlayerPrefs.SetInt("br4", PlayerPrefs.GetInt("br3"));
            PlayerPrefs.SetInt("br3", PlayerPrefs.GetInt("br2"));
            PlayerPrefs.SetInt("br2", PlayerPrefs.GetInt("br1"));
            PlayerPrefs.SetInt("br1", PlayerPrefs.GetInt("br0"));
            PlayerPrefs.SetInt("br0", PlayerPrefs.GetInt("Save_Round"));

            PlayerPrefs.SetInt("bk4", PlayerPrefs.GetInt("bk3"));
            PlayerPrefs.SetInt("bk3", PlayerPrefs.GetInt("bk2"));
            PlayerPrefs.SetInt("bk2", PlayerPrefs.GetInt("bk1"));
            PlayerPrefs.SetInt("bk1", PlayerPrefs.GetInt("bk0"));
            PlayerPrefs.SetInt("bk0", PlayerPrefs.GetInt("Save_Kill"));
            PlayerPrefs.SetInt("Log", logNum);
        }
    }
}
