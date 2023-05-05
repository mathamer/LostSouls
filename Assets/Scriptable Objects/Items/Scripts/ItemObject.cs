using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Tool,
    Object,
    Consumable
}

public abstract class ItemObject : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;
    [TextArea(15, 20)]
    public string description;
    
}
