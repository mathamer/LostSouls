using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressAndMusicBox : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().enabled = false;
    }
    void Update()
    {
        if (States.instance.dressGiven && States.instance.musicBoxGiven)
        {
            GetComponent<AudioSource>().enabled = true;
            transform.GetChild(2).gameObject.SetActive(true);

            States.instance.SecondSoulQuest = true;

            StartDancing();
        }
    }

    void StartDancing()
    {
        transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, Mathf.PingPong(Time.time, 0.5f) + 0.5f, transform.GetChild(0).position.z);
    }
}
