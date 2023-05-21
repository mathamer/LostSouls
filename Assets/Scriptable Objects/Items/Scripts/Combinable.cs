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

}

