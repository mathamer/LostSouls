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
        }
    }
}
