using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextComponent : MonoBehaviour
{
    GameObject target;
    Transform textHead;
    float rotSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        textHead = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player").gameObject;
        HeadRot();
    }

    void HeadRot()
    {
        Vector3 temp = target.transform.position;
        Vector3 dir = textHead.position - temp;
        Vector3 rot = Vector3.RotateTowards(textHead.forward, dir, rotSpeed * Time.deltaTime, 0);
        textHead.rotation = Quaternion.LookRotation(rot);
    }
}
