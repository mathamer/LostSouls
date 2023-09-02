using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSoulScript : MonoBehaviour
{
    void Update()
    {
        if (States.instance.ballThrown == true)
        {
            // activate sprite child
            transform.GetChild(0).gameObject.SetActive(true);

            // move dog to the left smoothly
            transform.GetChild(0).position = Vector3.MoveTowards(transform.GetChild(0).position, new Vector3(16, transform.GetChild(0).position.y, transform.GetChild(0).position.z), 0.1f);
            // make dog float up and down all the time
            transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, Mathf.PingPong(Time.time, 0.5f) + 0.5f, transform.GetChild(0).position.z);
        }
    }
}
