using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public AudioClip pickupSound;

        private void OnMouseDown()
    {
        // use distance of sprite wich is child of item gameobject to player to determine if player can pick up item
        if (Vector3.Distance(transform.GetChild(0).position, GameObject.FindGameObjectWithTag("Player").transform.position) < 3)
        {
            var item = GetComponent<Item>();
            Player.instance.inventory.AddItem(item.item, 1);
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
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