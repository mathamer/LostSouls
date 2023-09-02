using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollarScript : MonoBehaviour
{
    void Update()
    {
        if (States.instance.dogRopeCut)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<BoxCollider>().enabled = true;
        }
    }
}
