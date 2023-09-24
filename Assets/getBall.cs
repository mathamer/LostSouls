using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getBall : MonoBehaviour
{
    void Update()
    {
        if (States.instance.collarGiven)
        {
            transform.GetChild(3).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
