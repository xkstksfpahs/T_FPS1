using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponent : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    weaponFire wf;
    Aim aim;
    float xx, yy, zz;
    float ox, oy, oz;
    float missSpeed = 20;
    float rotSpeed = 15;

    public GameObject go;
    public GameObject go2;
    bool istrack = true;
    Transform istr;
    Transform tr;
    AudioSource mainSound;
    public AudioClip fireSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        wf = player.GetComponentInChildren<weaponFire>();
        aim = wf.GetComponent<Aim>();

        Vector3 dir = new Vector3(aim.dx, aim.dy, aim.dz);
        dir = dir.normalized;
        rb.velocity = dir * missSpeed;
        istr = GameObject.Find("wall").GetComponent<Transform>();
        tr = GetComponent<Transform>();
        mainSound = GetComponent<AudioSource>();
        mainSound.volume = PlayerPrefs.GetFloat("backVol", 1f);
        mainSound.PlayOneShot(fireSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        if (istrack)
        Tracking();
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void Tracking()
    {
        ox = aim.dx;
        oy = aim.dy;
        oz = aim.dz;
        Vector3 oPos = transform.position;
        xx = ox - oPos.x;
        yy = oy - oPos.y;
        zz = oz - oPos.z + 0.5f;
        Vector3 dir = new Vector3(xx, yy, zz);
        dir = dir.normalized;
        rb.velocity = dir * missSpeed;

        Vector3 rotDir = oPos - aim.obj;
        Vector3 rot = Vector3.RotateTowards(tr.forward, rotDir, rotSpeed * Time.deltaTime, 0);
        tr.rotation = Quaternion.LookRotation(rot);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject boom = Instantiate(go);
        boom.transform.position = transform.position;
        boom.transform.rotation = istr.transform.rotation;
        GameObject boom2 = Instantiate(go2);
        boom2.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 5f);
        boom2.transform.rotation = istr.transform.rotation;
        istrack = false;
        Destroy(gameObject);
    }
}
