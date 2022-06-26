using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomPlace : MonoBehaviour
{
    Player player;
    weaponFire wf;

    int dmg;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        wf = player.GetComponentInChildren<weaponFire>();
        dmg = wf.Dmg + 15;
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<Box>().Td(dmg);
        }
    }
}
