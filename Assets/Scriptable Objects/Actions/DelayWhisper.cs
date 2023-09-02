using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayWhisper : MonoBehaviour
{
    public AudioClip delayedClip;
    public float delayInSeconds = 2.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Calculate the delay time based on the desired delayInSeconds
        double delayTime = AudioSettings.dspTime + delayInSeconds;

        // Play the delayed sound
        audioSource.clip = delayedClip;
        audioSource.PlayScheduled(delayTime);
    }
}
