using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockTheater : MonoBehaviour
{
    void Update()
    {
        if (States.instance.firstKeyUsed && States.instance.secondKeyUsed && States.instance.thirdKeyUsed)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<UnityEngine.AI.NavMeshObstacle>().enabled = false;
        }
    }
}
