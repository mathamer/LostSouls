using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    void Update()
    {
        // Check if crowOnMonster is true
        if (States.instance.crowOnSpider)
        {
            // Remove the BoxCollider component if it exists
            BoxCollider boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Destroy(boxCollider);
            }
        }
    }
}
