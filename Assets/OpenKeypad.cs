using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenKeypad : MonoBehaviour
{
    public GameObject Keypad; // Reference to the Keypad GameObject

    void OnMouseDown()
    {
        if (Keypad != null && !States.instance.correctPasscode)
        {
            Debug.Log("ne radi");
            Keypad.SetActive(true); // Activate the Keypad GameObject
        }
        else
        {
            Debug.LogWarning("Keypad reference not set in the inspector!");
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
