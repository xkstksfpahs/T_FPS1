using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBullet : MonoBehaviour
{
    public int dmg = 1;
    Rigidbody rb;
    float speed = 2000;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Box>().Td(dmg);
        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver) return;
        Destroy(gameObject, 2f);
    }
}
