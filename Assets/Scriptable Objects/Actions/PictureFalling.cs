using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFalling : MonoBehaviour
{
    Animator anim1;
    [SerializeField] private Animator anim2;
    void Start()
    {
        anim1 = GetComponent<Animator>();
    }
    private void OnMouseDown()
    {
        if (anim1 != null)
        {
            Debug.Log("slikaa");
            anim1.SetBool("playAnimation", true);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim1.SetTrigger("pictureFalling");
        }
    }

}
