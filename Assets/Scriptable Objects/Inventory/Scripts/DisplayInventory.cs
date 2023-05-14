using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject Empty;

    private int emptySlots = 10;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        AddEmptySlots();

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            // Line below is for displaying the amount of items in the inventory
            // obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name.ToString();
            itemsDisplayed.Add(inventory.Container[i], obj);
        }
    }

    public void RemoveEmptySlots()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.tag == "Empty")
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void AddEmptySlots()
    {
        GameObject go;
        for (int i = 0; i < emptySlots; i++) {
            go = Instantiate(Empty, Vector3.zero, Quaternion.identity) as GameObject;
            go.transform.SetParent(transform);
        }
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemsDisplayed.ContainsKey(inventory.Container[i]))
            {
                // itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name.ToString();
            }
            else
            {
                var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                // obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name.ToString();
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
        if (emptySlots != 10 - inventory.Container.Count)
        {
            emptySlots = 10 - inventory.Container.Count;
            RemoveEmptySlots();
            AddEmptySlots();
        }
    }
}
