using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostSoulVillage : MonoBehaviour
{
    void Update()
    {
        if (States.instance.FirstSoulQuest)
        {
            GetComponent<BoxCollider>().enabled = false;

            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
