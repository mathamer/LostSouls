using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovementAnimation : MonoBehaviour
{

    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Animator animator;

    void Update()
    {
        navMeshAgent = GetComponentInParent<UnityEngine.AI.NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.Log("No NavMeshAgent found in parent components!");
        }
        else
        {
            Vector3 velocity =  transform.InverseTransformDirection(navMeshAgent.velocity);
            animator.SetFloat("Horizontal", (float)Math.Round(velocity.x));
            animator.SetFloat("Vertical", (float)Math.Round(velocity.y));
            animator.SetFloat("Speed", (float)Math.Round(velocity.magnitude));
        }
    }
}
