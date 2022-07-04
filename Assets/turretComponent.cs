using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretComponent : MonoBehaviour
{
    GameObject target;
    public GameObject bullet;
    public GameObject firePos;
    Transform head;
    float rotSpeed = 5;
    float time = 0, firstTime=0;
    public float atkRate = 0.8f;
    public bool buyItem = false;
    bool canFire = false;
    float startFireCount = 3;

    weaponFire wf;

    AudioSource gunSound;
    public AudioClip gunClip,turretOnClip;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0).transform;
        gunSound = GetComponent<AudioSource>();
        wf = GameObject.Find("Player").GetComponentInChildren<weaponFire>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Ready");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        gunSound.volume = wf.gunAudio.value;
        if (buyItem)
            StartCoroutine(FireStart());
        if (buyItem && canFire)
        {
            time += Time.deltaTime;
            HeadRot();
            shoot();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target != null) return;
        if (other.CompareTag("Enemy") && !other.gameObject.GetComponent<Box>().isSearching && other.GetComponent<Box>().isLife)
        {
            target = other.gameObject;
            other.gameObject.GetComponent<Box>().isSearching = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(target != null)
        {
            if (!target.GetComponent<Box>().isLife)
            {
                target = null;
            }
        }
        if(target == null)
        {
            if (other.CompareTag("Enemy") && !other.gameObject.GetComponent<Box>().isSearching)
            {
                target = other.gameObject;
                other.gameObject.GetComponent<Box>().isSearching = true;
            }
        }
        else if(target.tag == "Untagged")
        {
            target = null;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == target)
        {
            target = null;
        }
    }

    void HeadRot()
    {
        if (target == null) return;
        Vector3 temp = target.transform.position;
        temp.y += 0.5f;
        Vector3 dir = temp - head.position;
        Vector3 rot = Vector3.RotateTowards(head.forward, dir, rotSpeed * Time.deltaTime, 0);
        head.rotation = Quaternion.LookRotation(rot);
    }

    void shoot()
    {
        if (target == null) return;
        firstTime += Time.deltaTime;
        if (time >= atkRate && firstTime >= 0.2f)
        {
            Instantiate(bullet, firePos.transform.position, head.GetChild(0).transform.rotation);
            time = 0;
            firstTime = 0;
            gunSound.PlayOneShot(gunClip);
        }
    }

    public void SetTurret()
    {
        anim.SetTrigger("TurretOn");
        gunSound.PlayOneShot(turretOnClip);
    }

    IEnumerator FireStart()
    {
        yield return new WaitForSeconds(3);
        canFire = true;
    }
}
