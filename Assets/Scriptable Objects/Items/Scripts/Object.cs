using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object", menuName = "Inventory System/Items/Object")]
public class Object : ItemObject
{
    public void Awake()
    {
        type = ItemType.Object;
    }
}
