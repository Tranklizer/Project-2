using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	public Player_Info playerInfo;
	public GameObject playerCamera;

	public Rigidbody rb;

	public bool touchingGround = true;

	int currentJumps;

	// Use this for initialization
	void Start () {
		//Get the rigidbody component and the child camera
		rb = GetComponent<Rigidbody> ();
		playerInfo = GameObject.Find ("Player_Manager").GetComponent<Player_Info>();
		playerCamera = GameObject.Find ("Main Camera");
	}
	

	// Update is called once per frame
	void FixedUpdate () {



		//Create data based on Mouse and Key Inputs
		float VerticalTranslation = Input.GetAxis ("Vertical") * playerInfo.moveSpeed;
		float HorizontalTranslation = Input.GetAxis ("Horizontal") * playerInfo.moveSpeed;
		float HorizontalRotation = Input.GetAxis ("Mouse X") * playerInfo.turnSpeed;
		float VerticalRotation = Input.GetAxis ("Mouse Y") * playerInfo.turnSpeed;

		transform.Translate (Vector3.forward * VerticalTranslation * Time.deltaTime);
		transform.Translate (Vector3.right * HorizontalTranslation * Time.deltaTime);
		transform.Rotate (Vector3.up * HorizontalRotation);

		playerCamera.transform.Rotate (Vector3.left * VerticalRotation);

		//Create limits for camera up and down rotation

		//If the camera rotates up farther than 315, change its x rotation to equal 315
		if (playerCamera.transform.localEulerAngles.x < 315.0f && playerCamera.transform.localEulerAngles.x > 60.0f) 
		{
			playerCamera.transform.localEulerAngles = new Vector3(315.0f, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
		}
		//If the camera rotates down farther than 40, change its x rotation to equal 40
		else if (playerCamera.transform.localEulerAngles.x > 40.0f && playerCamera && playerCamera.transform.localEulerAngles.x < 300.0f)
		{
			playerCamera.transform.localEulerAngles = new Vector3(40.0f, playerCamera.transform.localEulerAngles.y, playerCamera.transform.localEulerAngles.z);
		}

		Debug.Log (rb.velocity);
		//If the player hits Space and they have not exceeded the maximum jump count, they jump
		if(touchingGround && Input.GetKeyDown(KeyCode.Space))
		{
			if(currentJumps <= playerInfo.maxJumps) //The player has not exceeded the maximum jumps
			{
				
				rb.velocity = new Vector3(0,5,0);
				currentJumps ++;
			}
			else //don't allow the player to jump until they hit the ground
			{
				touchingGround = false;
			}
		}

	
	}

	void OnCollisionStay(Collision other)
	{
		Vector3 normal = other.contacts [0].normal;
		//Check if the slope of the surface is shallow enough for the player to jump off of
		if (Vector3.Angle(Vector3.down, normal) > playerInfo.maxAngle)
		{
			touchingGround = true;
		}
		else
		{
			touchingGround = false;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		currentJumps = 0;
		touchingGround = true;

	}
}
