using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DressSoundController : MonoBehaviour
{
    public ItemObject item;
    public AudioClip pickupSound;
    public AudioClip additionalSound;
    private AudioSource audioSource;


    private void OnMouseDown()
    {
        // use distance of sprite wich is child of item gameobject to player to determine if player can pick up item
        if (Vector3.Distance(transform.GetChild(0).position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
        {
            var item = GetComponent<Item>();
            Player.instance.inventory.AddItem(item.item, 1);

            if (audioSource != null) // Ensure that audioSource is not null
        {
            // Enable the audio source (if it's disabled) before playing the audio clip
            if (!audioSource.isPlaying)
            {
                audioSource.enabled = true; // Enable the audio source
                audioSource.Play(); // Play the audio clip
            }
        }
        else
        {
            Debug.LogError("AudioSource is null. Make sure it is attached to the dress prefab.");
        }

            Destroy(gameObject);
            Debug.Log("Item added to inventory");
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