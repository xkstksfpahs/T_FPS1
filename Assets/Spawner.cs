using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    Player pl;
    public int count = 0;
    SpawnManager sm;
    // Start is called before the first frame update

    private void Start()
    {
        pl = GameObject.Find("Player").GetComponent<Player>();
        sm = GetComponentInParent<SpawnManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        
    }

}
