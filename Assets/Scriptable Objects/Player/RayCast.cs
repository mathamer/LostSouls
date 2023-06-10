using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class RayCast : MonoBehaviour {
    private RaycastHit vision;
    public float rayLength;
    public float smooth = 1f;
    private Rigidbody grabbedObject;
    private NavMeshAgent navMeshAgent;
    private Quaternion targetRotation;
    public Animator animator;

    void Start () {
        rayLength = 4.0f;
        navMeshAgent = GetComponent<NavMeshAgent> ();
        navMeshAgent.angularSpeed = 0;
    }
    
    void Update () {
        // Check if pointer is over UI
        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }

        // Vector3 velocity =  transform.InverseTransformDirection(navMeshAgent.velocity);
        Vector3 velocity =  navMeshAgent.velocity;
        animator.SetFloat("Horizontal", velocity.x);
        animator.SetFloat("Vertical", velocity.y);
        animator.SetFloat("Speed", velocity.magnitude);
        // Debug.Log(velocity.x);
        
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;
        if (Input.GetButtonDown ("Fire1")) {
            if (Physics.Raycast(ray, out hit, 100) && hit.collider.CompareTag("Ground")) {
                navMeshAgent.destination = hit.point;
                navMeshAgent.isStopped = false;
            }
        }
    }
}
