using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstQuest : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (States.instance.FirstSoulQuest)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(false);

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
