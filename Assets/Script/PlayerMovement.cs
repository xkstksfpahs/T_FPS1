using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float speed = 10f;

    Vector3 movement;
    Vector3 dir;
    Transform tr;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        move(h, v);

        dir = Vector3.right * h + Vector3.forward * v;
        dir = Camera.main.transform.TransformDirection(dir);
        dir.Normalize();
    }

    void move(float _h, float _v)
    {
        if (_h == 0 && _v == 0)
        {
            //rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
        movement.Set(_h, 0, _v);
        movement = dir * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        
    }
}
