using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleComponent : MonoBehaviour
{
    [SerializeField] GameObject Op, Rk,OAR;
    [SerializeField] Slider Sound, Mouse;
    [SerializeField] Text LogText, BestText;
    public bool isOption = false;
    int[] boardRound = new int[5], boardKill = new int[5];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            Op.transform.GetChild(i).gameObject.SetActive(true);
        }
        for (int g = 0; g < 3; g++)
        {
            Rk.transform.GetChild(g).gameObject.SetActive(false);
        }
        Sound.value = PlayerPrefs.GetFloat("Save_Sound",1f);
        Mouse.value = PlayerPrefs.GetFloat("Save_Mouse",1f);
        BoardText();
    }

    // Update is called once per frame
    void Update()
    {
        if(isOption)
        {
            PlayerPrefs.SetFloat("Save_Sound", Sound.value);
        }
        if(isOption)
        {
            PlayerPrefs.SetFloat("Save_Mouse", Mouse.value);
        }
    }

    public void OptionButton()
    {
        for(int i = 0; i < 3; i++)
        {
            Op.transform.GetChild(i).gameObject.SetActive(true);
        }
        for(int g = 0; g < 3; g++)
        {
            Rk.transform.GetChild(g).gameObject.SetActive(false);
        }
    }

    public void RankButton()
    {
        for (int i = 0; i < 3; i++)
        {
            Op.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int g = 0; g < 3; g++)
        {
            Rk.transform.GetChild(g).gameObject.SetActive(true);
        }
        BoardText();
    }

    public void EscButton()
    {
        OAR.SetActive(false);
        isOption = false;
    }

    public void Screen1280W()
    {
        Screen.SetResolution(1280, 720, false);
    }
    public void Screen1280F()
    {
        Screen.SetResolution(1280, 720, true);
    }
    public void Screen1366W()
    {
        Screen.SetResolution(1366, 768, false);
    }
    public void Screen1366F()
    {
        Screen.SetResolution(1366, 768, true);
    }
    public void Screen1920W()
    {
        Screen.SetResolution(1920, 1080, false);
    }
    public void Screen1920F()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void BoardText()
    {
        boardRound[0] = PlayerPrefs.GetInt("br0");
        boardRound[1] = PlayerPrefs.GetInt("br1");
        boardRound[2] = PlayerPrefs.GetInt("br2");
        boardRound[3] = PlayerPrefs.GetInt("br3");
        boardRound[4] = PlayerPrefs.GetInt("br4");

        boardKill[0] = PlayerPrefs.GetInt("bk0");
        boardKill[1] = PlayerPrefs.GetInt("bk1");
        boardKill[2] = PlayerPrefs.GetInt("bk2");
        boardKill[3] = PlayerPrefs.GetInt("bk3");
        boardKill[4] = PlayerPrefs.GetInt("bk4");

        BestText.text = "라운드 = " + PlayerPrefs.GetInt("Save_BestRound").ToString() + "R"
            + "\n\n" + "킬수 = " + PlayerPrefs.GetInt("Save_BestKill").ToString() + "K";

        LogText.text = "1. 라운드 - " + boardRound[0].ToString() + "Round\n     " + "킬수 - " + boardKill[0].ToString() + "Kill"
            + "\n\n\n2. 라운드 - " + boardRound[1].ToString() + "Round\n     " + "킬수 - " + boardKill[1].ToString() + "Kill"
            + "\n\n\n3. 라운드 - " + boardRound[2].ToString() + "Round\n     " + "킬수 - " + boardKill[2].ToString() + "Kill"
            + "\n\n\n4. 라운드 - " + boardRound[3].ToString() + "Round\n     " + "킬수 - " + boardKill[3].ToString() + "Kill"
            + "\n\n\n5. 라운드 - " + boardRound[4].ToString() + "Round\n     " + "킬수 - " + boardKill[4].ToString() + "Kill";
    }
}
