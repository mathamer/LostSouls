using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogCollision : MonoBehaviour
{
    public AudioSource dogCollisionSource;
    private bool hasPlayed = false;

    void Start()
    {
        dogCollisionSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider collision)
    {
        if (!hasPlayed && collision.CompareTag("Player"))
        {
            dogCollisionSource.Play();
            hasPlayed = true;
        }
    }
}
