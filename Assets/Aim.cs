using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public float dx, dy, dz;
    Player player;
    RaycastHit hit;
    float maxDistabce = 5000f;
    public Vector3 obj;
    [SerializeField] GameObject rp;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        RayCast();
    }

    void RayCast()
    {
        if (Physics.Raycast(rp.transform.position, transform.forward, out hit))
        {
            obj = hit.transform.InverseTransformPoint(hit.point);
            obj = hit.point;
            if (obj != null)
            {
                dx = obj.x;
                dy = obj.y;
                dz = obj.z;
            }
        }
    }
}
