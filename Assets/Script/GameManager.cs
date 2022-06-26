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
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        shopTr = GameObject.Find("Shop").GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        gameM = this;
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        if(isRound == true)
        {
            roundTime -= Time.deltaTime;
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
        if (shopSp)
            SpawnShop();
        if (shopTime <= 0)
        {
            isRound = true;
            roundTime = 60f;
            shopTime = 30f;
            round++;
            sm.bossSpawn = true;
        }
    }

    public void RoundTime()
    {
        timeText.text = "Round : "+round.ToString() +"\n"+roundTime.ToString("F1");
        //for (int i = 0; i < shops.Length; i++)
        //{
        //    shops[i].GetComponent<Shop>().isBuy = false;
        //}
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
        shopSp = false;
    }
}
