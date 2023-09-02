using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject Empty;

    private int emptySlots = 10;
    private int newEmptySlots;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        RemoveAllItems();
        UpdateDisplay();
        //CreateDisplay();
    }

    void Update()
    {
        UpdateDisplay();
    }

    public void CreateDisplay()
    {
        // AddEmptySlots();

        for (int i = 0; i < inventory.Container.Count; i++)
        {
            var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
            // Line below is for displaying the amount of items in the inventory
            // obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name.ToString();
            if (inventory.Container[i].amount > 0)
            {
                itemsDisplayed.Add(inventory.Container[i], obj);
            }
        }
    }

    public void RemoveAllItems()
    {

        Debug.Log("remvoe ALL ITEMS CALLED");
        foreach (Transform child in transform)
        {
            if (!child.GetComponent<DontDestroyOnLoad>())
            {
                Debug.Log(child.gameObject);
                Destroy(child.gameObject);
            }
        }
    }

    // public void RemoveEmptySlots()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         if (child.gameObject.tag == "Empty")
    //         {
    //             Destroy(child.gameObject);
    //         }
    //     }
    // }

    // public void AddEmptySlots()
    // {
    //     GameObject go;
    //     for (int i = 0; i < emptySlots; i++)
    //     {
    //         go = Instantiate(Empty, Vector3.zero, Quaternion.identity, transform);
    //         go.transform.SetAsLastSibling();
    //     }

    // }

    public void UpdateDisplay()
    {
        // newEmptySlots = 10;
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (inventory.Container[i].amount > 0)
            {
                // newEmptySlots -= 1;
                if (itemsDisplayed.ContainsKey(inventory.Container[i]))
                {
                    if (inventory.Container[i].amount > 1)
                    {
                        itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    }
                    else
                    {
                        itemsDisplayed[inventory.Container[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name.ToString();
                    }
                }
                else
                {
                    var obj = Instantiate(inventory.Container[i].item.prefab, Vector3.zero, Quaternion.identity, transform);
                    // obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].amount.ToString("n0");
                    obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.Container[i].item.name.ToString();
                    itemsDisplayed.Add(inventory.Container[i], obj);
                }
            }
        }
        // if (emptySlots != newEmptySlots)
        // {
        //     emptySlots = newEmptySlots;
        //     RemoveEmptySlots();
        //     AddEmptySlots();
        // }
    }
}
