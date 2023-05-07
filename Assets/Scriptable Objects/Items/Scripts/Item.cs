using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    public AudioClip pickupSound;

        private void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5)
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