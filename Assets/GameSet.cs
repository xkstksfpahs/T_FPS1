using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSet : MonoBehaviour
{
    public static GameSet gms;

    public bool gameStart = false;
    public bool gameOver = false;
    public bool esc = false;
    public bool tools = false;
    public bool isMouse = true;
    bool escvol = false, overVol = false;

    GameObject GS;
    GameObject ESCUI;
    GameObject GO;
    public Text endScoreText,TOne,TTwo;
    Text ingDmg, ingRound;
    weaponFire wf;
    public Slider escAudio,gameEndAudio,escMouse;
    toolTipComponent ttc;
    public int killCount;
    // Start is called before the first frame update
    void Start()
    {
        gms = this;
        //GS = GameObject.Find("GameStartUI").gameObject;
        ESCUI = GameObject.Find("ESCUI").gameObject;
        GO = GameObject.Find("GameOverUI").gameObject;
        wf = GameObject.Find("Player").GetComponentInChildren<weaponFire>();
        ttc = GameObject.Find("ToolTipUI").GetComponent<toolTipComponent>();

        gameStart = true;
        //GS.SetActive(false);
        isMouse = false;
        MouseOnOff();
        ttc.isTT = true;
        ttc.toolNum = 3;
        overVol = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ESCMenu();
        }
        if(escvol == true)
        {
            wf.Vol = escAudio.value;
            wf.gunAudio.value = escAudio.value;
            //gunAudio.value = escAudio.value;
            //PlayerPrefs.SetFloat("backVol", wf.Vol);
            PlayerPrefs.SetFloat("Save_Sound", wf.Vol);
            PlayerPrefs.SetFloat("Save_Mouse", escMouse.value);
        }
        if (!overVol)
        {
            gameEndAudio.value = PlayerPrefs.GetFloat("Save_Sound");
        }
        else if (overVol)
        {
            PlayerPrefs.SetFloat("Save_Sound", gameEndAudio.value);
        }
    }

    public void GameStart()
    {
        //SceneManager.LoadScene("FPS00");
        gameStart = true;
        GS.SetActive(false);
        isMouse = false;
        MouseOnOff();
        ttc.isTT = true;
        ttc.toolNum = 3;
    }

    public void GameOver()
    {
        overVol = true;
        isMouse = true;
        MouseOnOff();
        gameOver = true;
        if (GameManager.gameM.round > PlayerPrefs.GetInt("Save_BestRound"))
        {
            PlayerPrefs.SetInt("Save_BestRound", GameManager.gameM.round);
            TOne.text = "최고기록!";
        }
        else
            TOne.text = "기록유지";
        if (killCount > PlayerPrefs.GetInt("Save_BestKill"))
        {
            PlayerPrefs.SetInt("Save_BestKill", killCount);
            TTwo.text = "최고기록!";
        }
        else
            TTwo.text = "기록유지";
        PlayerPrefs.SetInt("Save_Round", GameManager.gameM.round);
        PlayerPrefs.SetInt("Save_Kill", killCount);
        PlayerPrefs.SetInt("Log", 1);
        for (int i=0; i < 8; i++)
        {
            GO.transform.GetChild(i).gameObject.SetActive(true);
        }
        endScoreText.text = "최고기록 : " + PlayerPrefs.GetInt("Save_BestRound").ToString() + "R" + "\n나의기록 : " + GameManager.gameM.round.ToString() + "R\n\n\n"
            + "최고킬수 : " + PlayerPrefs.GetInt("Save_BestKill").ToString() + "K" + "\n나의킬수 : " + killCount.ToString()+"K";


    }

    public void ReStart()
    {
        SceneManager.LoadScene("FPS00");
    }

    public void Exit()
    {
        SceneManager.LoadScene("Title");
    }
    public void ESCMenu()
    {
        if (gameStart)
        {
            if (!esc)
            {
                isMouse = true;
                escvol = true;
                for (int i = 0; i < 7; i++)
                {
                    ESCUI.transform.GetChild(i).gameObject.SetActive(true);
                    escAudio.value = PlayerPrefs.GetFloat("Save_Sound");
                    escMouse.value = PlayerPrefs.GetFloat("Save_Mouse");
                }
                esc = true;
                MouseOnOff();
                ingDmg = GameObject.Find("DMGText").GetComponent<Text>();
                ingRound = GameObject.Find("RAKText").GetComponent<Text>();
                ingDmg.text = "공격력 : " + wf.Dmg.ToString();
                ingRound.text = "현재라운드 : " + GameManager.gameM.round.ToString() + "\n\n" + "킬수 : " + killCount.ToString();
            }
            else if (esc)
            {
                isMouse = false;
                escvol = false;
                for (int i = 0; i < 7; i++)
                {
                    ESCUI.transform.GetChild(i).gameObject.SetActive(false);
                }
                esc = false;
                MouseOnOff();
            }
        }
    }
    public void MouseOnOff()
    {
        if (isMouse == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (isMouse == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
