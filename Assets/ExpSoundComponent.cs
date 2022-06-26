using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpSoundComponent : MonoBehaviour
{
    AudioSource ac;
    public AudioClip expSound;
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<AudioSource>();
        ac.volume = PlayerPrefs.GetFloat("backVol", 1f);
        ac.PlayOneShot(expSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
