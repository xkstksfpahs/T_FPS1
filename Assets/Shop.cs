using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public bool isDmgShop, isShotspeedShop, isTurretShop, isTurretDmgShop, isTurretSSShop, isMissileShop, isWeaponUpShop;
    public int shopCount;
    public bool isBuy = true;

    int needPoint = 30,speedPoint = 100,turretPoint = 150, turretDmgPoint = 30, turretSSPoint = 100, missilePoint = 30, WPPoint = 200;
    int dmgLevel=0, speedLevel=0;
    int ARdmg = 2, ARspeed = 2;

    public GameObject turret1,turret2;
    public GameObject TB;
    int onTurret = 0;
    [SerializeField] GameObject[] turrets;
    GameObject turretMain;

    weaponFire wf;
    public TextMesh pointText;
    Player pl;
    LauncherComponent lc;
    toolTipComponent ttc;
    GunsTip gt;

    public int gunsLevel = 1;
    Text mainText;
    public GameObject TextGameObject;
    float textSpawnTime = 2.5f;
    void Start()
    {
        wf = GameObject.Find("Player").GetComponentInChildren<weaponFire>();
        pl = wf.GetComponentInParent<Player>();
        lc = GameObject.Find("Player").GetComponentInChildren<LauncherComponent>();
        ttc = GameObject.Find("ToolTipUI").GetComponent<toolTipComponent>();
        gt = GameObject.Find("GunsTip").GetComponent<GunsTip>();
        mainText = GameObject.Find("MainText").GetComponent<Text>();
        TextGameObject.SetActive(false);
        if (shopCount == 0)
            isDmgShop = true;
        else if (shopCount == 1)
            isShotspeedShop = true;
        else if (shopCount == 2)
        {
            isTurretShop = true;

            turretMain = GameObject.Find("Turrets");
            for (int i = 0; i < turretMain.transform.childCount; i++)
            {
                turrets[i] = turretMain.GetComponent<turretMain>().turret[i];
            }
        }
        else if (shopCount == 3)
            isTurretDmgShop = true;
        else if (shopCount == 4)
            isTurretSSShop = true;
        else if (shopCount == 5)
            isMissileShop = true;
    }

    // Update is called once per frame
    void Update()
    {
        isBuy = GameManager.gameM.isBuy;
        if (textSpawnTime > 0)
            textSpawnTime -= Time.deltaTime;
        else if(textSpawnTime <= 0)
        {
            TextGameObject.SetActive(true);
        }
        if (isBuy)
        {
            if (isDmgShop)
            {
                pointText.text = "공격력\nPoint = " + needPoint.ToString();
            }
            else if (isShotspeedShop)
            {
                pointText.text = "무기구매\nPoint = " + speedPoint.ToString();
            }
            else if (isTurretShop)
            {
                if (onTurret < turrets.Length)
                    pointText.text = "터렛\nPoint = " + turretPoint.ToString();
                else if (onTurret >= turrets.Length)
                    pointText.text = "터렛\n최대치";
            }
            else if (isTurretDmgShop)
            {
                pointText.text = "터렛 공격력\nPoint = " + turretDmgPoint.ToString();
            }
            else if (isTurretSSShop)
            {
                pointText.text = "터렛 공격속도\nPoint = " + turretSSPoint.ToString();
            }
            else if (isMissileShop)
            {
                pointText.text = "미사일\nPoint = " + missilePoint.ToString();
            }
            else if (isWeaponUpShop)
            {
                pointText.text = "Weapon Up\nPoint = " + WPPoint.ToString();
            }
        }
        else
        {
            pointText.text = null;
            GetComponent<ShopMovement>().isMove = true;
        }
    }


    public void Shopping()
    {
        if (isDmgShop)
        {
            if (pl.point >= needPoint)
            {
                pl.point -= needPoint;
                wf.Dmg += 1;
                needPoint += 10;
                wf.dmgLevel++;
            }
            else
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
        else if(isShotspeedShop)
        {
            if(pl.point >= speedPoint && wf.wpLevel < 4)
            {
                pl.point -= speedPoint;
                wf.wpLevel++;
                gt.weaponNum++;
                gt.WeaponNum();
                ttc.isTT = true;
                ttc.toolNum = 4;
            }
            else if(wf.wpLevel >= 4)
            {
                mainText.text = "무기를 모두 개방했습니다.";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
            else if(pl.point < speedPoint)
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
        else if (isTurretShop)
        {
            if (pl.point >= turretPoint && onTurret < turrets.Length)
            {
                ttc.isTT = true;
                ttc.toolNum = 1;

                pl.point -= turretPoint;
                turretPoint += 30;
                turrets[onTurret].GetComponent<turretComponent>().buyItem = true;
                turrets[onTurret].GetComponent<turretComponent>().SetTurret();
                onTurret++;

                //if (onTurret == turrets.Length)
                //{
                //    turretPoint = 9999;
                //}
            }
            else if(onTurret >= turrets.Length)
            {
                mainText.text = "터렛이 최대치입니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
            else if(pl.point < turretPoint && onTurret < turrets.Length)
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
        else if (isTurretDmgShop)
        {
            if (pl.point >= turretDmgPoint)
            {
                pl.point -= turretDmgPoint;
                TB.GetComponent<turretBullet>().dmg++;
                turretDmgPoint += 10;
            }
            else
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
        else if (isTurretSSShop)
        {
            if(pl.point >= turretSSPoint)
            {
                pl.point -= turretSSPoint;
                turret1.GetComponent<turretComponent>().atkRate *= 0.9f;
                turret2.GetComponent<turretComponent>().atkRate *= 0.9f;
                turretSSPoint += 30;
            }
            else
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
        else if (isMissileShop)
        {
            if(pl.point >= missilePoint && lc.ammor < 1)
            {
                pl.point -= missilePoint;
                lc.ammor++;
                ttc.isTT = true;
                ttc.toolNum = 0;
            }
            else if(lc.ammor == 1)
            {
                mainText.text = "이미 미사일을 보유하고 있습니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
            else if(pl.point < missilePoint)
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
        else if (isWeaponUpShop)
        {
            if (pl.point >= WPPoint && wf.dmgLevel >= ARdmg && wf.speedLevel >= ARspeed)
            {
                wf.WeaponLevel = 2;
                WPPoint = 999;
            }
            else if (pl.point < WPPoint)
            {
                mainText.text = "포인트가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
            else if (wf.dmgLevel < ARdmg || wf.speedLevel < ARspeed)
            {
                mainText.text = "업그레이드가 부족합니다";
                mainText.color = new Color(mainText.color.r, mainText.color.g, mainText.color.b, 1);
            }
        }
    }
}
