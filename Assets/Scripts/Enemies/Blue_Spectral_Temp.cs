using UnityEngine;
using System.Collections;

public class Blue_Spectral_Temp : MonoBehaviour {
	public Transform[] wanderPoints = new Transform[12];
	public int currentWanderPoint = 0;
	Transform target;

	
	public float moveSpeed;
	public float turnSpeed;
	
	public int state = 0; //0 = wandering, 1 = attacking
	public Animator animator;
	
	float attackTimer;
	float turnTimer;
	

	public GameObject player;
	public GameObject E;
	public Alpha_Animate EScript;
	
	// Use this for initialization
	void Start () {
		target = wanderPoints [currentWanderPoint];
		EScript = E.GetComponent<Alpha_Animate> ();
		animator = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 relativePos = target.position - transform.position;
		Quaternion turnRotation = Quaternion.LookRotation (relativePos);
		transform.Translate (Vector3.forward * moveSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, turnRotation, turnSpeed * Time.deltaTime);
		
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		
		if (state == 0) // Wandering
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
		
		else if (state == 1) // attacking player
		{
			attackTimer += Time.deltaTime;

			if (animator.GetCurrentAnimatorStateInfo(0).IsName("Spot_Target"))
			{
				target = player.transform;
				moveSpeed = 0;
				turnSpeed = 2;
			}

			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
			{
				target = player.transform;
				moveSpeed = 8;
				turnSpeed = 2;
				if(Vector3.Distance(transform.position, target.position) < 7)
				{
					animator.SetTrigger("AttackTarget");
				}
			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
			{
				target = player.transform;
				moveSpeed = 8;
				turnSpeed = 2;
			}

			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_P2"))
			{
				moveSpeed = 0;
				turnSpeed = 1;
			}

			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Wander") && attackTimer >= 3.0f)
			{
				attackTimer = 0.0f;
				state = 0; //Go back to wandering
			}
			/*turnTimer += Time.deltaTime;
			if(turnTimer >= 1.0f)
			{
				state = 2;
				turnTimer = 0.0f;
			}*/
		}


		else if (state == 3) //Attacking Spectral
		{
			attackTimer += Time.deltaTime;

			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Spot_Target"))
			{
				moveSpeed= 1;
				turnSpeed = 2;
			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
			{
				moveSpeed = 6;
				turnSpeed = 2;
				if(Vector3.Distance(transform.position, target.position) < 6)
				{
					animator.SetTrigger("AttackTarget");
				}
			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
			{
				moveSpeed = 4;
				turnSpeed = 2;
			}

			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_P2"))
			{
				moveSpeed = 0;
				turnSpeed = 1;
			}
			else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Wander") && attackTimer > 3.0f)
			{
				Debug.Log("Reached wander state");
				attackTimer = 0.0f;
				state = 0;
				target = wanderPoints[currentWanderPoint];
			}
		}

		
	}
	
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.gameObject.SendMessage("IncreaseHit");
		}
		else if (other.gameObject.tag == "Building")
		{
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("Charge"))
			{
				animator.SetTrigger("HitObstacle");
				state = 0;
			}

			state = 0;
		}

		else if (other.gameObject.tag == "RedSpectral")
		{
			other.gameObject.SendMessage("RespawnSpectral");
			EScript.isActive = true;
			EScript.opacity = 1.0f;
			EScript.delay = 0.0f;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && state == 0) 
		{
			attackTimer = 0.0f;
			animator.SetTrigger("SeeTarget");
			state = 1;
		}

		else if(other.gameObject.tag == "RedSpectral" && state == 0)
		{
			attackTimer = 0.0f;
			animator.SetTrigger("SeeSpectral");
			state = 3;
			target = other.gameObject.transform;

		}
	}
}
