﻿using UnityEngine;
using System.Collections;

public class Spectral_Anim : MonoBehaviour {

	public Transform[] wanderPoints = new Transform[12];
	public int currentWanderPoint = 0;
	Transform target;

	public float moveSpeed;
	public float turnSpeed;

	public int state = 0; //0 = wandering, 1 = attacking

	float attackTimer;

	public Animator anim;
	public GameObject player;

	// Use this for initialization
	void Start () {
		target = wanderPoints [currentWanderPoint];
		anim = this.gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 relativePos = target.position - transform.position;
		Quaternion turnRotation = Quaternion.LookRotation (relativePos);
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, turnRotation, turnSpeed * Time.deltaTime);

		Vector3 forward = transform.TransformDirection(Vector3.forward);

		if (state == 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Wander"))
		{
			target = wanderPoints[currentWanderPoint];

			moveSpeed = 3;
			turnSpeed = 1;

			RaycastHit hit;
			Ray sight = new Ray(transform.position, forward);

			if(Physics.Raycast(sight, out hit, 23.0f))
			{
				if (hit.collider.tag == "Player")
				{
					state = 1;
					anim.SetTrigger("SeePlayer");
					attackTimer = 0.0f;
				}
			}

			// Switch the target patrol point if the Spectral gets too close
			if (Vector3.Distance (transform.position, target.position) <= 3) 
			{
				currentWanderPoint ++; //cycle through the wander point
				if (currentWanderPoint >= 12) 
				{
					currentWanderPoint = 0;
				}
				target = wanderPoints [currentWanderPoint];
			}
		}

		if (state == 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("Charge_WindUp"))
		{
			target = player.transform;
			moveSpeed = 1;
		}

		else if (state == 1)
		{
			target = player.transform;
			moveSpeed = 8;
			turnSpeed = 3;
			attackTimer += Time.deltaTime;
			if (attackTimer > 3.0f)
			{
				state = 0;
				anim.SetTrigger("LostPlayer");
				target = wanderPoints[currentWanderPoint];
			}
			else if (Vector3.Distance (transform.position, target.position) <= 8)
			{
				anim.SetTrigger("AttackPlayer");
				state = 0;
			}
		}

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Left_Swipe")) 
		{
			turnSpeed = 0;
			moveSpeed = 1;
		}
	
	}


	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log("Player is Dead");
		}
		if (other.gameObject.tag == "Building")
		{
			state = 0;
		}
	}
}