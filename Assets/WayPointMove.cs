using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointMove : MonoBehaviour
{
    [SerializeField] SphereCollider[] wayPointGroup;
    float speed;
    Transform[] wayPoint;
    int wayPointNum = 1;

    Transform head;
    GameObject target;
    float rotSpeed = 5;
    Box bx;
    // Start is called before the first frame update
    void Start()
    {
        wayPointGroup = GameObject.Find("MapSpawnPoint").GetComponentsInChildren<SphereCollider>();
        wayPoint = wayPointGroup[Random.Range(0, wayPointGroup.Length)].GetComponentsInChildren<Transform>();
        head = transform.GetChild(2).transform;
        bx = GetComponent<Box>();
        if (bx.isBoss)
        {
            if (bx.istank)
            {
                speed = 1.5f;
            }
            else if (bx.isspeeder)
            {
                speed = 9f;
            }
        }
        else if (!bx.isBoss)
        {
            speed = 3;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver || GameSet.gms.tools) return;
        if (bx.isLife)
        {
            MovePath();
            HeadRot();
        }
    }

    void MovePath()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, wayPoint[wayPointNum].transform.position, speed * Time.deltaTime);
        target = wayPoint[wayPointNum].gameObject;
        if (transform.position == wayPoint[wayPointNum].transform.position)
            wayPointNum++;
        if (wayPointNum == wayPoint.Length)
            wayPointNum = 1;
    }

    void HeadRot()
    {
        Vector3 temp = target.transform.position;
        Vector3 dir = temp - head.position;
        Vector3 rot = Vector3.RotateTowards(head.forward, dir, rotSpeed * Time.deltaTime, 0);
        head.rotation = (Quaternion.LookRotation(rot));
    }
}
