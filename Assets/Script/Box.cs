using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    int hp;

    public TextMesh boxtext;
    GameManager gm;
    Player pl;
    weaponFire wf;
    SpawnManager sm;

    AudioSource boxSound;

    public AudioClip boxClip;

    public bool isSearching = false;
    public bool isLife = true;
    bool boxType;
    int g;

    MeshRenderer mrr;
    public GameObject spaceShip;
    public bool isBoss = false;
    public bool istank, isspeeder;
    Transform tr;
    [SerializeField] GameObject[] DeadParticles;
    toolTipComponent ttc;
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        wf = GameObject.Find("Player").GetComponentInChildren<weaponFire>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        g = Random.Range(0, 100);
        ttc = GameObject.Find("ToolTipUI").GetComponent<toolTipComponent>();
        if (!isBoss)
        {
            if(gm.round == 1)
            {
                hp = 1;
            }
            else if (g >= 80 && gm.round > 1)
            {
                boxType = false;
                hp = Random.Range(1, 4);
            }
            else
            {
                boxType = true;
                hp = gm.round + Random.Range(0, 3) -1;
            }
            sm.count++;
        }
        else if (isBoss)
        {
            boxType = true;
            if (istank)
            {
                hp = gm.round * 12;
            }
            else if (isspeeder)
            {
                hp = gm.round * 3;
            }
            sm.count += 5;
        }
        boxSound = GetComponent<AudioSource>();
        //mrr = GetComponent<MeshRenderer>();
        mrr = spaceShip.GetComponent<MeshRenderer>();
        //head = spaceShip.GetComponent<Transform>();
    }

    void Update()
    {
        boxSound.volume = PlayerPrefs.GetFloat("Save_Sound");
        Text();
        MatColor();
    }

    public void Dead()
    {
        isLife = false;
        pl.point++;
        if (pl.killPoint < 100 && wf.istracking == false)
            pl.killPoint += 2;

        tr = GetComponent<Transform>();
        if (!isBoss)
        {
            int i = Random.Range(0, 3);
            GameObject go = Instantiate(DeadParticles[0]);
            go.transform.position = tr.transform.position;
            go.transform.rotation = tr.transform.rotation;
            //go.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
            //Debug.Log(i);
        }
        else if (isBoss)
        {
            GameObject go = Instantiate(DeadParticles[3]);
            go.transform.position = tr.transform.position;
            //go.transform.rotation = tr.transform.rotation;
        }
        transform.position =new Vector3(1.5f,-20.5f,-7.5f);
        boxSound.PlayOneShot(boxClip);
        GameSet.gms.killCount++;
        Destroy(gameObject, 0.5f);
        if (!isBoss)
            sm.count--;
        else if (isBoss)
            sm.count -= 5;
    }

    public void Td(int a)
    {
        if (boxType == true)
        {
            hp -= a;
            if (hp <= 0 && isLife)
            {
                Dead();
            }
        }
        else if (boxType == false)
        {
            hp--;
            if (hp <= 0 && isLife)
            {
                Dead();
            }
        }
        if (isBoss)
        {
            ttc.isTT = true;
            ttc.toolNum = 2;
        }
    }

    public void Text()
    {
        boxtext.text = hp.ToString();
        if (hp <= 0)
        {
            boxtext.text = null;
        }
    }
    void MatColor()
    {
        //if (boxType == true)
        //{
        //    mrr.material.color = new Color(79f, 202f, 41f);
        //}
        if(boxType == false)
        {
            mrr.material.color = Color.red;
        }
    }
}
