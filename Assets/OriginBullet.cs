using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginBullet : MonoBehaviour
{
    weaponFire wf;
    Rigidbody rb;
    float fireSd = 300f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        wf = GameObject.Find("Gun").GetComponent<weaponFire>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 1f);
        Destroy(gameObject, 2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (wf.issr)
                collision.gameObject.GetComponent<Box>().Td(wf.srDmg);
            else
                collision.gameObject.GetComponent<Box>().Td(wf.Dmg);
        }
        Destroy(gameObject);
    }
}
