using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFalling : MonoBehaviour
{
    Animator anim1;
    private AudioSource glassBreak;
    private bool hasSoundPlayed = false;
    void Start()
    {
        anim1 = GetComponent<Animator>();
        glassBreak = GetComponent<AudioSource>();
    }
    private void OnMouseDown()
    {
        if (anim1 != null)
        {
            Debug.Log("slikaa");
            anim1.SetBool("playAnimation", true);
            if (!hasSoundPlayed && glassBreak != null)
            {
                Invoke("PlaySound", 0.7f);
                hasSoundPlayed = true;
            }
        }
    }
    void PlaySound()
    {
        if (glassBreak != null)
        {
            glassBreak.Play();
        }
    }

    private void OnMouseOver()
    {
        GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }

}
