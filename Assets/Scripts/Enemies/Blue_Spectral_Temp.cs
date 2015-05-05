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
		
		else if (state == 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Spot_Target")) // turning to player
		{
			target = player.transform;
			moveSpeed = 0;
			turnSpeed = 2;
			/*turnTimer += Time.deltaTime;
			if(turnTimer >= 1.0f)
			{
				state = 2;
				turnTimer = 0.0f;
			}*/
		}

		else if (state == 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Charge")) //Attacking Player
		{
			target = player.transform;
			moveSpeed = 8;
			turnSpeed = 2;
			attackTimer += Time.deltaTime;
			if (attackTimer > 3.0f)
			{
				state = 0;
				animator.SetTrigger("HitObstacle");
				target = wanderPoints[currentWanderPoint];
			}
			if(Vector3.Distance(transform.position, target.position) < 5)
			{
				animator.SetTrigger("AttackTarget");
			}
		}

		else if (state == 1 && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_P2"))
		{
			target = player.transform;
			moveSpeed = 0;
			turnSpeed = 2;
		}


		else if (state == 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Spot_Target")) //turning to Red Spectral
		{
			moveSpeed = 0;
			turnSpeed = 2;
			/*turnTimer += Time.deltaTime;
			if(turnTimer >= 1.3f)
			{
				state = 4;
			}*/
		}

		else if (state == 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Charge")) //Attacking Red Spectral
		{
			moveSpeed = 7;
			turnSpeed = 2;
			attackTimer += Time.deltaTime;
			if (attackTimer > 3.0f)
			{
				state = 0;
				animator.SetTrigger("HitObstacle");
				target = wanderPoints[currentWanderPoint];
			}
			if(Vector3.Distance(transform.position, target.position) < 5)
			{
				Debug.Log("Close to target");
				animator.SetTrigger("AttackTarget");
			}
		}
		else if (state == 3 && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_P2"))
		{
			moveSpeed = 0;
			turnSpeed = 2;
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
			EScript.isActive = true;
			EScript.opacity = 1.0f;
			EScript.delay = 0.0f;
			target = wanderPoints[currentWanderPoint];
			state = 0;
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && (state != 1 || state != 3)) 
		{
			animator.SetTrigger("SeeTarget");
			state = 1;
		}

		else if(other.gameObject.tag == "RedSpectral" && (state != 3 || state != 1))
		{
			animator.SetTrigger("SeeTarget");
			state = 3;
			target = other.gameObject.transform;

		}
	}
}
