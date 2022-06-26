using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SRShoot : MonoBehaviour
{
    AudioSource gunPlayer;
    Animator gunAnim;
    public AudioClip reloadSound;
    public GameObject firePos;
    public GameObject flashEffect;

    public bool isFire = false, isReload = false;
    // Start is called before the first frame update
    void Start()
    {
        gunPlayer = GetComponent<AudioSource>();
        gunAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver) return;
        if (isFire)
        {
            flash();
            isFire = !isFire;
        }

        if (isReload)
        {
            gunPlayer.PlayOneShot(reloadSound);
            gunAnim.SetTrigger("Reload");
            isReload = false;
        }
    }

    void flash()
    {
        GameObject muzzle = Instantiate(flashEffect, firePos.transform.position, firePos.transform.rotation);
        Destroy(muzzle, 2f);
    }
}
