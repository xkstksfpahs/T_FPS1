using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARShoot : MonoBehaviour
{
    public GameObject muzzleFlashBang;

    private Animator GunAnimator;
    weaponFire wf;
    public bool isReload = false, isFire = false;
    public GameObject firePos;
    // Start is called before the first frame update
    void Start()
    {
        GunAnimator = GetComponent<Animator>();
        wf = GetComponentInParent<weaponFire>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver) return;
        if (Input.GetButton("Fire1") && wf.arFireDelay <= 0 && wf.arMagBullet > 0 && isFire)
        {
            GunAnimator.SetTrigger("Fire");
            Flash();
            isFire = !isFire;
        }
        if (isReload)
        {
            GunAnimator.SetTrigger("Reload");
            isReload = false;
        }
    }

    void Flash()
    {
        GameObject flash;
        flash = Instantiate(muzzleFlashBang, firePos.transform.position, firePos.transform.rotation);
        Destroy(flash, 2f);
    }
}
