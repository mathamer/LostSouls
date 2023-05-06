using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RayCast : MonoBehaviour {
	private RaycastHit vision;
	public float rayLength;
	public float smooth = 1f;
	private Rigidbody grabbedObject;
	private NavMeshAgent navMeshAgent;
	private Quaternion targetRotation;

	void Start () {
		rayLength = 4.0f;
		navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.angularSpeed = 0;
	}
	
	void Update () {
	    Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
	    RaycastHit hit;
	    if (Input.GetButtonDown ("Fire1")) {
	        if (Physics.Raycast(ray, out hit, 100)) {
	            if (hit.collider.CompareTag("plane")) {
					navMeshAgent.destination = hit.point;
					navMeshAgent.isStopped = false;
	            }
	        }
	    }
	}
}
