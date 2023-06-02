using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    public AudioSource creationSource;
    public AudioClip creationClip;
    public AudioSource dragSource;
    public AudioClip dragClip;

    void Start()
    {
        creationSource.PlayOneShot(creationClip);
    }

    public void playAudio(bool draggedOnPlayer) {
        if (draggedOnPlayer) {
            dragSource.PlayOneShot(dragClip);
        }
    }
}
