using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSearch : MonoBehaviour
{

    public bool CI = true;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(CI);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CI);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Turret"))
            CI = false;
        Debug.Log(CI);
    }
    private void OnTriggerExit(Collider other)
    {
        CI = true;
        Debug.Log(CI);
    }
}
