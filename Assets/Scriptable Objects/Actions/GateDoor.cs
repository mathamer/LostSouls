using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDoor : MonoBehaviour
{
    public Vector3 openOffset; // The offset to move the fence piece from its current position when opening
    public float movementSpeed = 1f; // The speed at which the fence piece moves

    private Vector3 initialPosition; // The initial position of the fence piece
    private bool isOpen = false; // Flag to track if the fence piece is open

    public AudioSource gateSliding;
    public AudioClip gateSlidingClip;

    private void Start()
    {
        initialPosition = transform.position;

        if (gateSliding == null) 
        {
            gateSliding = GetComponent<AudioSource>();
             gateSliding.clip = gateSlidingClip;
        }

    }

    private void Update()
    {
        if (isOpen)
        {
            MoveTo(initialPosition + openOffset);
        }
        else
        {
            MoveTo(initialPosition);
        }
    }

    private void MoveTo(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
    }

    public void Interact()
    {
        if (isOpen)
        {
            isOpen = false;
        }
        else
        {
            isOpen = true;
        }

        if(gateSliding != null)
        {
            gateSliding.Play();
        }
    }
}
