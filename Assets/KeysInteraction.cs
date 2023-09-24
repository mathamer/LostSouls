using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(States.instance.firstKeyUsed) {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        if(States.instance.secondKeyUsed) {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }


}
