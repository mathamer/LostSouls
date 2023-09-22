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
            if (spiderAudio != null && clip != null)
            {
                Debug.Log("spiderAudio and clip are not null!");
                spiderAudio.clip = clip;
                spiderAudio.Play();
            }
            else
            {
                Debug.Log("spiderAudio or clip is null!");
                spiderAudio.Stop();
            }
        }
    }
}
