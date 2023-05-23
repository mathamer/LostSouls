using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Combinable : MonoBehaviour
{
    public List<string> combinableWithNames;
    public string inputItem;
    public ItemObject result;
    public AudioClip combineSound;
    // Add public for required amount of items to combine
    public int requiredAmount;

    private void Start()
{
    // Set input item to the name of the current GameObject
    string[] nameParts = gameObject.name.Split(new string[] { "(Clone)" }, System.StringSplitOptions.None);
    inputItem = nameParts[0];
}

    private void OnMouseOver()
    {
        GetComponentInChildren<Renderer>().material.color = Color.red;
    }

    private void OnMouseExit()
    {
        GetComponentInChildren<Renderer>().material.color = Color.white;
    }

    // If the item XyloParts has amount of 4, replace it item Xylophone

    // private void Update()
    // {
    //     if (Player.instance.inventory.Container.Count > 0)
    //     {
    //         //go through inventory in for loop and check what contianer id is and if amount is 4
    //         //if true, remove 4 xylo parts and add xylophone
    //         for (int i = 0; i < Player.instance.inventory.Container.Count; i++)
    //         {
    //             if (Player.instance.inventory.Container[i].ID == 4 && Player.instance.inventory.Container[i].amount == 4)
    //             {
    //                 Player.instance.inventory.RemoveItem(gameObject.GetComponent<Combinable>().inputItem, 4);
    //                 Player.instance.inventory.AddItem(gameObject.GetComponent<Combinable>().result, 1);
    //             }
    //         }
    //     }
    // }

}

