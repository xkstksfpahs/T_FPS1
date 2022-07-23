using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int point = 0, dmg = 1, life = 30;
    float lifeTime = 2f;
    public float killPoint;
    public Text pointText;
    public Slider kpSlider;
    SpawnManager sm;
    weaponFire wf;
    bool istracking = false;
    public Image bloodS;
    ShakeCamera shake;
    toolTipComponent ttc;
    AudioSource ads;
    public AudioClip dmgAC;
    // Start is called before the first frame update
    void Start()
    {
        sm = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        wf = GetComponentInChildren<weaponFire>();
        shake = GetComponentInChildren<ShakeCamera>();
        ttc = GameObject.Find("ToolTipUI").GetComponent<toolTipComponent>();
        ads = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        Text();
        if(sm.count >= 20)
        {
            dLife();
        }
        kpSlider.value = killPoint * 0.01f;
        SetTracking();
        TrackingOn();
        if(killPoint >= 100 && !ttc.active)
        {
            ttc.active = true;
            ttc.isTT = true;
            ttc.toolNum = 5;
        }
        ads.volume = PlayerPrefs.GetFloat("Save_Sound");
    }
    void Text()
    {
        pointText.text = ("Life = "+life.ToString() +"\nPoint = " + point.ToString());
    }

    public void dLife()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            life--;
            ads.PlayOneShot(dmgAC);
            StartCoroutine(ShowBloodScreen());
            //StartCoroutine(shake.ShakeCameras());
            lifeTime = 2f;
            if(life <= 0)
            {
                life = 0;
                GameSet.gms.GameOver();
            }
        }
    }
    public void SetTracking()
    {
        if (killPoint >= 100 && Input.GetKeyDown(KeyCode.F) && wf.ishg == false)
        {
            wf.istracking = true;
            istracking = true;
        }
    }
    public void TrackingOn()
    {
        if (istracking)
        {
            if(killPoint > 0)
            {
                killPoint -= Time.deltaTime * 8;
            }
            else if (killPoint <= 0)
            {
                killPoint = 0;
                wf.istracking = false;
                istracking = false;
            }
        }
    }
    IEnumerator ShowBloodScreen()
    {
        bloodS.color = new Color(1, 0, 0, Random.Range(0.2f, 0.3f));
        yield return new WaitForSeconds(0.1f);
        bloodS.color = Color.clear;
    }
    
}
