using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int dmg = 1;
    Player pl;
    Rigidbody rb;
    float sspeed = 2f,startspeed = 200;

    private float dis, speed, waitTime;
    public Transform targetTr;
    Transform tr;
    GameObject target;
    float rotspeed = 150;

    public float turn;
    public float ballVelocity;
    void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        dmg += pl.dmg;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * startspeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSet.gms.gameStart || GameSet.gms.esc || GameSet.gms.gameOver) return;
        HeadRot();
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null)
        {
            if (other.CompareTag("Enemy"))
            {
                //obj = other.GetComponent<GameObject>();
                //followTarget(other.GetComponent<GameObject>());
                //Debug.Log(other);
                target = other.gameObject;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Box>().Td(dmg);
        }
        else if(collision.gameObject.tag == "EnemyChild")
        {
            collision.gameObject.GetComponentInParent<Box>().Td(dmg);
        }
        Destroy(gameObject);
    }

    void DiffusionBullet_Move()
    {
        if (targetTr == null) return;

        waitTime += Time.deltaTime;
        if (waitTime < 1.5f)
        {
            speed = Time.deltaTime;
            transform.Translate(tr.forward * speed, Space.World);
        }
        else
        {
            speed += Time.deltaTime;
            float t = speed / dis;

            tr.position = Vector3.LerpUnclamped(tr.position, targetTr.position, t);
        }

        Vector3 directionVec = targetTr.position - tr.position;
        Quaternion qua = Quaternion.LookRotation(directionVec);
        tr.rotation = Quaternion.Slerp(tr.rotation, qua, Time.deltaTime * 2f);
    }

    void HeadRot()
    {
        if (target == null) return;

        Vector3 temp = target.transform.position;

        //temp.y += 0.5f;

        Vector3 dir = (temp - tr.position).normalized;

        Vector3 rot = Vector3.RotateTowards(tr.forward, dir, rotspeed * Time.deltaTime, 0);

        tr.rotation = Quaternion.LookRotation(rot);

        rb.AddForce(transform.forward * sspeed, ForceMode.VelocityChange);
    }
}
