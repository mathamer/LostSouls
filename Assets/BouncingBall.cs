using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float bounceHeight = 1.0f;  // Adjust this value to control the bounce height.
    public float bounceSpeed = 1.0f;   // Adjust this value to control the bounce speed.

    private Vector3 originalPosition;
    
    void Start()
    {
        // Store the original position of the ball.
        originalPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position based on a sine wave.
        float newY = originalPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        
        // Update the ball's position.
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}