using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy,Boss1,Boss2;

    public Transform[] sPoint;
    GameManager gm;

    public int count = 0;
    public int maxBoxCount = 30;

    float createTime = 1.8f;
    public bool bossSpawn;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sPoint = GetComponentsInChildren<Transform>();
        //BossSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        if (gm.isRound == true)
        {
            createTime -= Time.deltaTime;
            if (createTime <= 0)
            {
                Spawn();
                createTime = 1;
            }
        }
        if (bossSpawn)
        {
            bossSpawn = !bossSpawn;
            BossSpawn();
        }
    }

    void Spawn()
    {
        int i = Random.Range(1, sPoint.Length);
        if (count < maxBoxCount)
        {
            Instantiate(enemy, sPoint[i].position, sPoint[i].rotation);
        }
    }
    void BossSpawn()
    {
        int i = Random.Range(0, 2);
        if (i == 0)
            go = Boss1;
        else if (i == 1)
            go = Boss2;
        Instantiate(go, sPoint[2].position, sPoint[2].rotation);
    }
}
