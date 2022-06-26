using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMovement : MonoBehaviour
{

    Rigidbody rb;
    float fireSd = 0.1f;
    public bool isMove = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetTrigger("Come");
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
            outside();
    }

    void outside()
    {
        rb.AddForce(transform.forward * fireSd, ForceMode.Impulse);
        Destroy(gameObject, 10f);
    }
}
