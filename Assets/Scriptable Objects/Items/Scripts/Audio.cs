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
        if (creationClip != null)
        {
            creationSource.PlayOneShot(creationClip);
        }
        else
        {
            Debug.Log("creationClip is null");
        }

    }

    public void playAudio(bool draggedOnPlayer)
    {
        if (draggedOnPlayer)
        {
            dragSource.PlayOneShot(dragClip);

            Combinable combinable = gameObject.GetComponent<Combinable>();

            // If the dragged item is a BloodyXylo, then activate the GateDoors
            if (gameObject.GetComponent<Combinable>().inputItem == "BloodyXylo")
            {
                GateDoor[] gateDoors = FindObjectsOfType<GateDoor>();
                foreach (GateDoor gateDoor in gateDoors)
                {
                    // trigger it after 3 seconds
                    gateDoor.Invoke("Interact", 3);
                }
                // Destroy gameObject after playing the audio and remove the item from the inventory
                Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 1);
                Destroy(gameObject, dragClip.length);
                // TODO: Drop the item on the ground
            }
        }
    }
}
