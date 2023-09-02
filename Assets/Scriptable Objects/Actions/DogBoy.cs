using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBoy : MonoBehaviour
{
    void Update()
    {
        if (States.instance.ballThrown)
        {
            GetComponent<BoxCollider>().enabled = true;

            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, new Vector3(12, transform.GetChild(0).position.y, transform.GetChild(0).position.z), 0.06f);

            GetComponent<BoxCollider>().transform.position = Vector3.MoveTowards(GetComponent<BoxCollider>().transform.position, new Vector3(20, GetComponent<BoxCollider>().transform.position.y, GetComponent<BoxCollider>().transform.position.z), 0.06f);
            // it doesnt work
            // GetComponent<BoxCollider>().transform.position = Vector3.Lerp(GetComponent<BoxCollider>().transform.position, new Vector3(12, GetComponent<BoxCollider>().transform.position.y, GetComponent<BoxCollider>().transform.position.z), 0.06f);
        }
    }
}
