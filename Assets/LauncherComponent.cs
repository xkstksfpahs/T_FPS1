using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherComponent : MonoBehaviour
{
    private Animator Anime;
    //public bool isReady = false;
    public int ammor = 0;
    // Start is called before the first frame update
    void Start()
    {
        Anime = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ammor >= 1)
        {
            Anime.SetBool("isReady", true);
        }
        else
        {
            Anime.SetBool("isReady", false);
        }
    }
}
