using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePosTracking : MonoBehaviour
{
    Aim aim;
    float xx, yy, zz;
    Transform tr;
    float rotSpeed = 15;
    // Start is called before the first frame update
    void Start()
    {
        aim = GameObject.Find("Gun").GetComponent<Aim>();
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Tracking();
    }

    void Tracking()
    {
        Vector3 oPos = transform.position;
        xx = (aim.dx - oPos.x)+0.1f;
        yy = (aim.dy - oPos.y)+0.05f;
        zz = (aim.dz - oPos.z);
        Vector3 dir = new Vector3(xx, yy, zz);
        //dir = dir.normalized;

        Vector3 rotDir = oPos - aim.obj;
        Vector3 rot = Vector3.RotateTowards(tr.forward, dir, rotSpeed * Time.deltaTime,0);
        tr.rotation = Quaternion.LookRotation(rot);
    }
}
