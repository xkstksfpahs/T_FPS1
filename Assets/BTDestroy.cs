using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDestroy : MonoBehaviour
{
    float DestoryTime = 0.1f;
    Rigidbody rb;
    float fireSd = 200;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * fireSd, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        DestoryTime -= Time.deltaTime;
        if (DestoryTime <= 0)
            Destroy(gameObject);
    }
}
