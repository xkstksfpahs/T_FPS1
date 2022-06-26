using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunsTip : MonoBehaviour
{
    public int weaponNum = -1;
    [SerializeField] GameObject[] go;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WeaponNum()
    {
        for(int i = 0; i< go.Length; i++)
        {
            go[i].SetActive(false);
        }
        go[weaponNum].SetActive(true);
        //Debug.Log(weaponNum);
    }
}
