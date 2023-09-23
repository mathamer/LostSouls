using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderEating : MonoBehaviour
{
    public AudioSource spiderAudio;
    public AudioClip clip;


    void Update()
    {
        if (States.instance.crowOnSpider)
        {
            GetComponent<AudioSource>().enabled = true;
        }
    }
}
