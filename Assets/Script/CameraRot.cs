using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    float rx, ry;
    public float rotSpeed;
    PlayerMovement pm;
    GameObject gun,launcher;
    Transform tr;
    ShakeCamera sc;
    public float thisx = 0,thisy=0;
    public float countx = 0, CCx = 0;
    float returnX = 0;
    void Start()
    {
        pm = GetComponentInParent<PlayerMovement>();
        gun = GameObject.Find("Gun").gameObject;
        launcher = GameObject.Find("Launcher").gameObject;
        tr = GetComponent<Transform>();
        sc = GetComponent<ShakeCamera>();
        if(PlayerPrefs.GetFloat("Save_Mouse") == 0)
        {
            PlayerPrefs.SetFloat("Save_Mouse", 0.5f);
        }
        rotSpeed = PlayerPrefs.GetFloat("Save_Mouse") * 400;
    }

    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.tools || GameSet.gms.gameOver) return;
        if(rotSpeed != PlayerPrefs.GetFloat("Save_Mouse") * 400)
        {
            rotSpeed = PlayerPrefs.GetFloat("Save_Mouse") * 400;
        }
        if(CCx > 0)
        {
            CCx -= Time.deltaTime * 150;
            returnX = 0.5f;
        }
        else if(CCx <= 0)
        {
            CCx = 0;
            returnX = 0;
        }
        sc.tr = tr.transform;
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        rx += rotSpeed * my * Time.deltaTime - thisx - returnX;
        ry += rotSpeed * mx * Time.deltaTime - thisy;

        rx = Mathf.Clamp(rx, -80, 80);

        transform.eulerAngles = new Vector3(-rx, ry, 0);
        pm.transform.eulerAngles = new Vector3(0, ry, 0);
        gun.transform.eulerAngles = new Vector3(-rx, ry - 1, 0);
        launcher.transform.eulerAngles = new Vector3(-rx, ry - 1, 0);
    }
}
