using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;

        private void OnMouseDown()
    {
        if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) < 5)
        {
            var item = GetComponent<Item>();
            // inventory.AddItem(item.item, 1); // need to add it to the inventory on Player.cs
            Player.instance.inventory.AddItem(item.item, 1);


            Destroy(gameObject);
            Debug.Log("Key picked up");
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
