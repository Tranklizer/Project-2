using UnityEngine;
using System.Collections;

public class Blue_Spectral_Temp : MonoBehaviour {
	public Transform[] wanderPoints = new Transform[12];
	public int currentWanderPoint = 0;
	Transform target;

	
	public float moveSpeed;
	public float turnSpeed;
	
	public int state = 0; //0 = wandering, 1 = attacking
	
	float attackTimer;
	

	public GameObject player;
	
	// Use this for initialization
	void Start () {
		target = wanderPoints [currentWanderPoint];
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 relativePos = target.position - transform.position;
		Quaternion turnRotation = Quaternion.LookRotation (relativePos);
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, turnRotation, turnSpeed * Time.deltaTime);
		
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		
		if (state == 0)
		{
			target = wanderPoints[currentWanderPoint];
			
			moveSpeed = 3;
			turnSpeed = 1;
			
			/*RaycastHit hit;
			Ray sight = new Ray(transform.position, forward);

			if(Physics.Raycast(sight, out hit, 23.0f))
			{
				if (hit.collider.tag == "Player")
				{
					state = 1;
					anim.SetTrigger("SeePlayer");
					attackTimer = 0.0f;
				}
			}*/
			
			// Switch the target patrol point if the Spectral gets too close
			if (Vector3.Distance (transform.position, target.position) <= 3) 
			{
				currentWanderPoint ++; //cycle through the wander point
				if (currentWanderPoint >= wanderPoints.Length)  
				{
					currentWanderPoint = 0;
				}
				target = wanderPoints [currentWanderPoint];
			}
		}
		
		else if (state == 1)
		{
			target = player.transform;
			moveSpeed = 7;
			turnSpeed = 2;
			attackTimer += Time.deltaTime;
			if (attackTimer > 3.0f)
			{
				state = 0;
				target = wanderPoints[currentWanderPoint];
			}
		}

		else if (state = 2) //Attacking Spectral
		{
			moveSpeed = 7;
			turnSpeed = 2;
			attackTimer += Time.deltaTime;
			if (attackTimer > 3.0f)
			{
				state = 0;
				target = wanderPoints[currentWanderPoint];
			}
		}

		
	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log("Player is Dead");
			Application.LoadLevel("Game_Over");
		}
		else if (other.gameObject.tag == "Building")
		{
			state = 0;
		}

		else if (other.gameObject.tag == "RedSpectral")
		{
			Destroy(other.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			state = 1;
		}

		else if(other.gameObject.tag == "RedSpectral")
		{
			state = 2;
			target = other.gameObject.transform;
		}
	}
}
