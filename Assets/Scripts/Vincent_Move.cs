using UnityEngine;
using System.Collections;

public class Vincent_Move : MonoBehaviour {

	public float moveSpeed;
	public float turnSpeed;
	public Transform target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		Vector3 relativePos = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation (relativePos);

		transform.rotation = Quaternion.Slerp (transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

		if(Vector3.Distance(transform.position, target.position) < 2.0f)
		{
			moveSpeed = 0;
		}

		else
		{
			moveSpeed = 3;
		}
	
	}
}
