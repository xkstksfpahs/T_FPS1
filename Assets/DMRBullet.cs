using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DMRBullet : MonoBehaviour
{
    weaponFire wf;
    public GameObject bulletFac;
    Transform[] firePos;

    // Start is called before the first frame update
    void Start()
    {
        wf = GameObject.Find("Gun").GetComponent<weaponFire>();
        firePos = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver) return;
        transform.Translate(Vector3.forward * 1f);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Box>().Td(wf.srDmg);
            if(wf.istracking && wf.issr)
            Fires();
            Destroy(gameObject);
        }
        else
        Destroy(gameObject);
    }
    void Fires()
    {
        for (int i = 1; i < firePos.Length; i++)
        {
            Instantiate(bulletFac, firePos[i].transform.position, firePos[i].transform.rotation);
        }
    }
}
