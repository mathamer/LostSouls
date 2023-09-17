using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSprite : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject objectWithSpriteRenderer;
    public GameObject sprite1; // Reference to the GameObject with the item
    public GameObject sprite2; // Reference to the GameObject without the item
    public string itemNameToCheck;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = objectWithSpriteRenderer.transform.localScale;
    }

    void Update()
    {
        bool hasItem = CheckForItem(itemNameToCheck);

        if (hasItem)
        {
            sprite2.SetActive(true);
            sprite1.SetActive(false);
        }
        else
        {
            sprite2.SetActive(false);
            sprite1.SetActive(true);
        }
    }

    bool CheckForItem(string itemName)
    {
        foreach (InventorySlot slot in inventory.Container)
        {
            if (slot.item != null && slot.item.name == itemName)
            {
                return true;
            }
        }
        return false;
    }
}
