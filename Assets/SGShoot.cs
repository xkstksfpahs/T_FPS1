using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGShoot : MonoBehaviour
{
    public GameObject MuzzleFlash;

    private Animator GunAnimator;
    weaponFire wf;
    public bool isReload = false, isFire = true;
    public GameObject firePos;
    bool isFull = true;
    public AudioSource gunPlayer;
    public AudioClip shoot, pump, reload;
    // Start is called before the first frame update
    void Start()
    {
        GunAnimator = GetComponent<Animator>();
        wf = GetComponentInParent<weaponFire>();
        gunPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver) return;
        if(isFire)
        {
            if(wf.istracking == false)
            GunAnimator.SetTrigger("Fire");
            Flash();
            gunPlayer.volume = PlayerPrefs.GetFloat("Save_Sound");
            wf.gunPlayer.PlayOneShot(shoot);
            wf.gunPlayer.PlayOneShot(pump);
            isFire = false;
        }
        if (wf.sgMagBullet != 8)
            isFull = false;
        else
            isFull = true;
        if (isReload)
        {
            GunAnimator.SetTrigger("Reload");
            isReload = false;
            GunAnimator.SetBool("isReload", false);
        }
        if(isFull == true)
        {
            GunAnimator.SetBool("isReload", true);
        }
    }
    void Flash()
    {
        GameObject flash;
        flash = Instantiate(MuzzleFlash, firePos.transform.position, firePos.transform.rotation);
        Destroy(flash, 2f);
    }
}
