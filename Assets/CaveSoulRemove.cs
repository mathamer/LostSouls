using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSoulRemove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (States.instance.CaveSoulQuest)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
