using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponFire : MonoBehaviour
{
    public float fireSpeed = 20, fireDelay, fireSd = 600, reloadTime = 1;
    public float arFireSpeed = 0.2f, arFireDelay, arFireSd = 600, arReloadTime = 2;
    public float sgFireSpeed = 1f, sgFireDelay, sgFireSd = 600, sgReloadTime = 1f;
    float srFireDelay;
    public float srReloadTime;
    public int mag = 8,magbullet;
    public int arMag = 50, arMagBullet;
    public int sgMag = 8, sgMagBullet;
    public int srMag = 10, srMagBullet;
    [SerializeField] bool isReload = false;
    public GameObject bulletFac, sgBullFac, dmrBulletFac;
    public GameObject firePos, missFirePos,sgFirePos,srFirePos;
    //public GameObject originBull;
    Transform ft;
    Player pl;
    LauncherComponent lc;
    ARShoot ars;
    SGShoot sgs;
    SRShoot srs;
    Animator anm;

    public GameObject MissFac;

    public float FD = 0.5f,TFD = 0.3f;
    public float arFD = 0.2f, sgFD = 0.7f,tsgFD = 0.2f,srFd = 0.1f;

    public GameObject trails;
    RaycastHit hit;
    float maxDistabce = 50f;
    public int Dmg = 1;
    public int srDmg;

    public AudioSource gunPlayer;
    public AudioClip gunSound, reloadSound, launcherSound,ARreloadSound,SRShootSound;
    public Slider gunAudio;
    TextMesh magtext;

    public float Vol = 1f;

    public bool istracking = false;
    ShakeCamera sc;
    CameraRot cr;

    public bool ishg, isar, issr, issg;
    bool stF = false;
    public int spdCount = 0;
    public GameObject hg, ar, sg, sr;
    public int WeaponLevel = 1, dmgLevel = 0, speedLevel = 0;
    Vector3 sgOriginPos;
    Quaternion sgOriginRot;
    public bool shakeRotate = true;

    [SerializeField] GameObject[] wpImage;
    public int wpLevel = 1;
    // Start is called before the first frame update
    void Start()
    {
        pl = GetComponentInParent<Player>();
        gunPlayer = GetComponent<AudioSource>();
        //Vol = PlayerPrefs.GetFloat("backVol", 1f);
        Vol = PlayerPrefs.GetFloat("Save_Sound", 1f);
        gunAudio.value = Vol;
        gunPlayer.volume = gunAudio.value;
        //escAudio.value = gunAudio.value;
        magbullet = mag;
        arMagBullet = arMag;
        sgMagBullet = sgMag;
        srMagBullet = srMag;
        magtext = GetComponentInChildren<TextMesh>();
        lc = GameObject.Find("Launcher").GetComponent<LauncherComponent>();
        sc = GameObject.Find("Player").GetComponentInChildren<ShakeCamera>();
        cr = GameObject.Find("Player").GetComponentInChildren<CameraRot>();
        ars = GetComponentInChildren<ARShoot>();
        sgs = GetComponentInChildren<SGShoot>();
        srs = GetComponentInChildren<SRShoot>();
        sgOriginPos = sgFirePos.transform.position;
        sgOriginRot = sgFirePos.transform.rotation;
        anm = GetComponent<Animator>();
        anm.SetBool("Ready", false);
    }

    // Update is called once per frame
    void Update()
    {
        MagText();
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        SoundSlider();
        anm.SetBool("Ready", true);
        Swap();
        if (Input.GetMouseButtonUp(1))
        {
            FireMissile();
        }
        ThisGunType();
        slotsColor();
        WPImage();
    }
    void trailRen()
    {
        if (ishg)
        {
            GameObject bull = Instantiate(trails);
            bull.transform.position = firePos.transform.position;
            bull.transform.rotation = firePos.transform.rotation;
            //Rigidbody rb = bull.GetComponent<Rigidbody>();
            //rb.AddForce(/*Camera.main.*/transform.forward* fireSd, ForceMode.Impulse);
        }
        else if (issr)
        {
            GameObject bull = Instantiate(trails);
            bull.transform.position = srFirePos.transform.position;
            bull.transform.rotation = srFirePos.transform.rotation;
            Rigidbody rb = bull.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * fireSd, ForceMode.Impulse);
        }
    }
    IEnumerator trailRenC()
    {
        yield return new WaitForSeconds(0.01f);
        GameObject bull = Instantiate(trails);
        bull.transform.position = ars.firePos.transform.position;
        bull.transform.rotation = ars.firePos.transform.rotation;
        //Rigidbody rb = bull.GetComponent<Rigidbody>();
        //rb.AddForce(firePos.transform.forward * fireSd, ForceMode.Impulse);
    }

    void SoundSlider()
    {
        gunPlayer.volume = gunAudio.value;
        Vol = gunAudio.value;
        //escAudio.value = gunAudio.value;
        //PlayerPrefs.SetFloat("backVol", Vol);
        PlayerPrefs.SetFloat("Save_Sound", Vol);
    }

    IEnumerator ReLoad()
    {
        if (isReload && ishg)
        {
            gunPlayer.PlayOneShot(reloadSound);
            yield return new WaitForSeconds(1.8f);
            magbullet = mag;
            isReload = !isReload;
        }
        else if (isReload && isar)
        {
            gunPlayer.PlayOneShot(ARreloadSound);
            yield return new WaitForSeconds(2.5f);
            arMagBullet = arMag;
            isReload = !isReload;
        }
        else if (isReload && issg)
        {
            yield return new WaitForSeconds(0.4f);
            for (int i = sgMagBullet; i < sgMag; i++)
            {
                yield return new WaitForSeconds(0.3f);
                sgMagBullet++;
                sgs.gunPlayer.PlayOneShot(sgs.reload);
            }
            isReload = !isReload;
        }
        else if (isReload && issr)
        {
            yield return new WaitForSeconds(2f);
            srMagBullet = srMag;
            isReload = !isReload;
        }
    }

    void MagText()
    {
        if (ishg)
            magtext.text = magbullet.ToString() + "/" + mag.ToString();
        else if (isar)
            magtext.text = arMagBullet.ToString() + "/" + arMag.ToString();
        else if (issg)
            magtext.text = sgMagBullet.ToString() + "/" + sgMag.ToString();
        else if (issr)
            magtext.text = srMagBullet.ToString() + "/" + srMag.ToString();
    }

    private void TrackingShoot()
    {
        GameObject bullet = Instantiate(bulletFac);
        bullet.transform.position = srFirePos.transform.position;
        bullet.transform.rotation = srFirePos.transform.rotation;
        arFireDelay = TFD;
        gunPlayer.PlayOneShot(gunSound);
        ars.Flash();
    }

    void RcShoot()
    {
        if (ishg)
        {
            if (Physics.Raycast(pl.transform.position, transform.forward, out hit/*, maxDistabce*/))
            {
                Debug.DrawRay(pl.transform.position, transform.forward * maxDistabce, Color.blue, 0.1f);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Box>().Td(Dmg);
                }
                else if (hit.transform.CompareTag("Shop"))
                {
                    hit.transform.GetComponent<Shop>().Shopping();
                }
            }
            fireDelay = FD;
            gunPlayer.PlayOneShot(gunSound);
            magbullet--;
            trailRen();
            if (magbullet <= 0 && !isReload)
            {
                isReload = true;
                StartCoroutine(ReLoad());
                GetComponentInChildren<SimpleShoot>().isReload = true;
            }
        }
        else if (isar)
        {
            if (Physics.Raycast(pl.transform.position, transform.forward, out hit))
            {
                Debug.DrawRay(pl.transform.position, transform.forward * maxDistabce, Color.blue, 0.1f);
                if (hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Box>().Td(Dmg);
                }
                else if (hit.transform.CompareTag("Shop"))
                {
                    hit.transform.GetComponent<Shop>().Shopping();
                }
            }
            arFireDelay = arFD;
            gunPlayer.PlayOneShot(gunSound);
            arMagBullet--;
            StartCoroutine("trailRenC");
            ars.isFire = true;
            if (arMagBullet <= 0)
            {
                isReload = true;
                StartCoroutine(ReLoad());
                ars.isReload = true;
            }
        }
        else if (issg)
        {
            if (Physics.Raycast(pl.transform.position, transform.forward, out hit))
            {
                if (hit.transform.CompareTag("Shop"))
                {
                    hit.transform.GetComponent<Shop>().Shopping();
                }
            }
            StartCoroutine(ShakeFire());
            StartCoroutine(ShakeFire());
            if (!istracking)
            {
                sgMagBullet--;
                sgFireDelay = sgFD;
            }
            else if (istracking)
            {
                sgFireDelay = tsgFD;
            }
            sgs.isFire = true;
            if(sgMagBullet <= 0 && !isReload)
            {
                isReload = true;
                StartCoroutine("ReLoad");
                sgs.isReload = true;
            }
        }
        else if (issr)
        {
            if(Physics.Raycast(pl.transform.position,transform.forward,out hit))
            {
                if (hit.transform.CompareTag("Shop"))
                {
                    hit.transform.GetComponent<Shop>().Shopping();
                }
            }
            Fire();
            srMagBullet--;
            srs.isFire = true;
            gunPlayer.PlayOneShot(SRShootSound);
            srFireDelay = srFd;
            if (srMagBullet <= 0 && !isReload)
            {
                isReload = true;
                StartCoroutine(ReLoad());
                srs.isReload = true;
            }
        }
    }

    void FireMissile()
    {
        if(lc.ammor > 0)
        {
            GameObject Missile = Instantiate(MissFac);
            Missile.transform.position = missFirePos.transform.position;
            Missile.transform.rotation = missFirePos.transform.rotation;
            gunPlayer.PlayOneShot(launcherSound);
            lc.ammor--;
            StartCoroutine(sc.ShakeCamerasMis());
        }
    }

    void ThisGunType()
    {
        if (ishg)
        {
            HandGun();
            hg.SetActive(true);
            ar.SetActive(false);
            sg.SetActive(false);
            sr.SetActive(false);
            
        }
        else if (isar)
        {
            AssaultRifle();
            hg.SetActive(false);
            ar.SetActive(true);
            sg.SetActive(false);
            sr.SetActive(false);
        }
        else if (issg)
        {
            ShotGun();
            hg.SetActive(false);
            ar.SetActive(false);
            sg.SetActive(true);
            sr.SetActive(false);
        }
        else if (issr)
        {
            DMR();
            hg.SetActive(false);
            ar.SetActive(false);
            sg.SetActive(false);
            sr.SetActive(true);
        }
    }
    public IEnumerator ShakeFire(float dur = 0, float magPos = 0, float magRot = 3)
    {
        for (int i = 0;i < 7; i++)
        {
            Fire();
            if (shakeRotate)
            {
                Vector3 shakeRot = new Vector3(Random.Range(-5, magRot), Random.Range(-5, magRot), Random.Range(0, 360));
                sgFirePos.transform.localRotation = Quaternion.Euler(shakeRot);
            }
            yield return new WaitForSeconds(dur);
        }
        sgFirePos.transform.localRotation = sgOriginRot;
    }

    public void Fire()
    {
        if (issg)
            Instantiate(sgBullFac, sgFirePos.transform.position, sgFirePos.transform.rotation);
        else if (issr)
            Instantiate(dmrBulletFac, srFirePos.transform.position, srFirePos.transform.rotation);
    }

    void Swap()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !istracking && wpLevel >= 1)
        {
            ishg = true;
            isar = false;
            issg = false;
            issr = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !istracking && wpLevel >= 2)
        {
            ishg = false;
            isar = true;
            issg = false;
            issr = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !istracking && wpLevel >= 3)
        {
            ishg = false;
            isar = false;
            issg = true;
            issr = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && !istracking && wpLevel >= 4)
        {
            ishg = false;
            isar = false;
            issg = false;
            issr = true;
        }
    }

    void HandGun()
    {
        if (fireDelay > 0)
            fireDelay -= Time.deltaTime;
        else if (fireDelay <= 0 && Input.GetMouseButton(0) && magbullet > 0)
        {
            if (!istracking)
            {
                RcShoot();
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReload && magbullet != mag)
        {
            magbullet = 0;
            isReload = true;
            GetComponentInChildren<SimpleShoot>().isReload = true;
            StartCoroutine(ReLoad());
        }
        if (FD <= 0.25f)
        {
            FD = 0.25f;
        }
    }

    void AssaultRifle()
    {
        if (arFireDelay > 0)
        {
            arFireDelay -= Time.deltaTime;
            cr.thisx = 0f;
        }
        else if (arFireDelay <= 0 && Input.GetMouseButton(0) && arMagBullet > 0)
        {
            cr.thisx = -1f;
            cr.thisy = Random.Range(-0.04f, 0.04f);
            cr.countx++;
            stF = true;
            if (!istracking)
            {
                RcShoot();
            }
            else if (istracking)
            {
                TrackingShoot();
            }
        }
        if (arMagBullet <= 0 && stF == true || Input.GetMouseButtonUp(0))
        {
            cr.CCx = cr.countx;
            cr.thisy = 0;
            cr.countx = 0;
            stF = false;
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReload && arMagBullet != arMag)
        {
            arMagBullet = 0;
            isReload = true;
            GetComponentInChildren<ARShoot>().isReload = true;
            StartCoroutine(ReLoad());
        }
    }
    void ShotGun()
    {
        if(sgFireDelay > 0)
        {
            sgFireDelay -= Time.deltaTime;
            cr.thisx = 0f;
        }
        else if (Input.GetMouseButtonDown(0)&& sgFireDelay <=0 && sgMagBullet > 0 && !istracking)
        {
            RcShoot();
            cr.thisx = -5f;
            cr.CCx = 8f;
        }
        else if (Input.GetMouseButton(0) && sgFireDelay <= 0 && sgMagBullet > 0 && istracking)
        {
            RcShoot();
            cr.thisx = -5f;
            cr.CCx = 8f;
        }
        if (Input.GetKeyDown(KeyCode.R)&& !isReload && sgMagBullet != sgMag)
        {
            isReload = true;
            StartCoroutine("ReLoad");
            sgs.isReload = true;
        }
        if(isReload && Input.GetMouseButtonDown(0)&& sgMagBullet > 0)
        {
            StopCoroutine("ReLoad");
            isReload = !isReload;
            sgs.isReload = false;
        }
    }
    void DMR()
    {
        if (srFireDelay > 0)
        {
            srFireDelay -= Time.deltaTime;
            cr.thisx = 0f;
        }
        if (Input.GetMouseButtonDown(0) && srMagBullet > 0)
        {
            srDmg = Dmg * 3;
            RcShoot();
            cr.thisx = -5f;
            cr.CCx = 8f;
        }
        if (Input.GetKeyDown(KeyCode.R) && !isReload && srMagBullet != srMag)
        {
            srMagBullet = 0;
            isReload = true;
            srs.isReload = true;
            StartCoroutine(ReLoad());
        }
    }
    public void WPImage()
    {
        if (wpLevel >= 2)
            wpImage[wpLevel-1].GetComponentInChildren<SphereCollider>().gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        if (wpLevel >= 3)
            wpImage[wpLevel-1].GetComponentInChildren<SphereCollider>().gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        if (wpLevel >= 4)
            wpImage[wpLevel-1].GetComponentInChildren<SphereCollider>().gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
    void slotsColor()
    {
        if (ishg && wpLevel >= 1)
        {
            for(int i = 0; i < wpImage.Length; i++)
            {
                wpImage[i].GetComponent<Image>().color = new Color(255, 255, 255);
            }
            wpImage[0].GetComponent<Image>().color = new Color(0, 70, 255);
        }
        else if (isar && wpLevel >= 2)
        {
            for (int i = 0; i < wpImage.Length; i++)
            {
                wpImage[i].GetComponent<Image>().color = new Color(255, 255, 255);
            }
            wpImage[1].GetComponent<Image>().color = new Color(0, 70, 255);
        }
        else if (issg && wpLevel >= 3)
        {
            for (int i = 0; i < wpImage.Length; i++)
            {
                wpImage[i].GetComponent<Image>().color = new Color(255, 255, 255);
            }
            wpImage[2].GetComponent<Image>().color = new Color(0, 70, 255);
        }
        else if (issr && wpLevel >= 4)
        {
            for (int i = 0; i < wpImage.Length; i++)
            {
                wpImage[i].GetComponent<Image>().color = new Color(255, 255, 255);
            }
            wpImage[3].GetComponent<Image>().color = new Color(0, 70, 255);
        }
    }
}
