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

            StartDancing();
        }
        if (States.instance.SecondSoulQuest)
        {
            // fade out the first child only once
            if (transform.GetChild(0).GetComponent<Renderer>().material.color.a == 1)
            {
                Dissapear();
            }
        }
    }

    void StartDancing()
    {
        transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, Mathf.PingPong(Time.time, 0.5f) + 0.5f, transform.GetChild(0).position.z);
    }

    void Dissapear()
    {
        StartCoroutine(FadeOutObjects());
    }

    IEnumerator FadeOutObjects()
    {
        // Fade out the first child
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = transform.GetChild(0).GetComponent<Renderer>().material.color;
            c.a = f;
            transform.GetChild(0).GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
