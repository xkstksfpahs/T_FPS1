using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameM;
    public int round = 1;
    float roundTime = 60f;
    float shopTime = 30f;

    public Text timeText;

    public bool isRound = true, isShop = false, shopSp = false;
    SpawnManager sm;
    [SerializeField] GameObject[] shops;
    Transform[] shopTr;
    public bool isBuy = false;
    toolTipComponent ttc;
    TimeTextComponent timeCom;
    bool three = false, two = false, one = false;
    AudioSource ads;
    public AudioClip startAC;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        shopTr = GameObject.Find("Shop").GetComponentsInChildren<Transform>();
        roundTime = 20;
        ttc = GameObject.Find("ToolTipUI").GetComponent<toolTipComponent>();
        timeCom = GameObject.Find("TimeText").GetComponent<TimeTextComponent>();
        ads = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        gameM = this;
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        if(isRound == true)
        {
            roundTime -= Time.deltaTime;
            timeCom.isround = true;
            if (roundTime <= 4 && three == false)
            {
                timeCom.SetColor(1);
                three = true;
            }
            else if (roundTime <= 3 && two == false)
            {
                timeCom.SetColor(1);
                two = true;
            }
            else if (roundTime <= 2 && one == false)
            {
                timeCom.SetColor(1);
                one = true;
            }
            RoundTime();
            isBuy = false;
        }
        if(roundTime <= 0)
        {
            isRound = false;
            ShopTime();
            isBuy = true;
        }
    }

    public void ShopTime()
    {
        shopTime -= Time.deltaTime;
        timeText.text = "Shopping"+"\n"+shopTime.ToString("F1");
        timeCom.isround = false;
        if (shopTime <= 4 && three == true)
        {
            timeCom.SetColor(2);
            three = false;
        }
        else if (shopTime <= 3 && two == true)
        {
            timeCom.SetColor(2);
            two = false;
        }
        else if (shopTime <= 2 && one == true)
        {
            timeCom.SetColor(2);
            one = false;
        }
        if (shopSp)
            SpawnShop();
        if (shopTime <= 0)
        {
            isRound = true;
            roundTime = 60f;
            shopTime = 30f;
            round++;
            sm.bossSpawn = true;
            ads.PlayOneShot(startAC);
        }
    }

    public void RoundTime()
    {
        timeText.text = "Round : "+round.ToString() +"\n"+roundTime.ToString("F1");
        shopSp = true;
    }

    public void SpawnShop()
    {
        for(int i = 0; i < shops.Length; i++)
        {
            GameObject go = Instantiate(shops[0]);
            go.GetComponent<Shop>().shopCount = i;
            go.GetComponent<Shop>().isBuy = true;
            go.transform.position = shopTr[i+1].transform.position;
            go.transform.rotation = shopTr[i+1].transform.rotation;
        }
        ttc.isTT = true;
        ttc.toolNum = 6;
        shopSp = false;
    }
}
