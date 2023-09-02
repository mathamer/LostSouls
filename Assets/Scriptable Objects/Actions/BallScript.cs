using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    void Update()
    {
        if (States.instance.ballThrown)
        {
            transform.GetChild(0).gameObject.SetActive(true);

            transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, new Vector3(transform.GetChild(0).position.x, transform.GetChild(0).position.y, -5), 0.1f);

            // make it bounce till it reaches position above 
            if (transform.GetChild(0).position.z >= -5)
            {
                transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, Mathf.PingPong(Time.time, 0.5f) + 0.5f, transform.GetChild(0).position.z);
            }
        }
    }
}
