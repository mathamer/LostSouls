using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    void Update()
    {
        if (States.instance.collarGiven)
        {
            GetComponent<BoxCollider>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
