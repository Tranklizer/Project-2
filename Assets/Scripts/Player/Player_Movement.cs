using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	public Player_Info playerInfo;
	public GameObject playerCamera;
	public GameObject Blood;
	public Alpha_Animate bloodAlpha;
	
	public Rigidbody rb;
	
	public bool touchingGround = true;
	
	int currentJumps;
	public bool isSprinting = false;
	public float movespeed;
	
	Vector3 currentPosition;
	Vector3 velocity;

	int hits = 0;
	
	// Use this for initialization
	void Start () {
		//Get the rigidbody component and the child camera
		rb = GetComponent<Rigidbody> ();
		playerInfo = GameObject.Find ("Player_Manager").GetComponent<Player_Info>();
		bloodAlpha = Blood.GetComponent<Alpha_Animate> ();
		playerCamera = GameObject.Find ("Main Camera");
	}
	


// Update is called once per frame
void FixedUpdate () {
	//Create data based on Mouse and Key Inputs
	float VerticalTranslation = Input.GetAxis ("Vertical") * playerInfo.moveSpeed;
	float HorizontalTranslation = Input.GetAxis ("Horizontal") * playerInfo.moveSpeed;
	float HorizontalRotation = Input.GetAxis ("Mouse X") * playerInfo.turnSpeed;
	float VerticalRotation = Input.GetAxis ("Mouse Y") * playerInfo.turnSpeed;
	
	//Move the player forward. This works regardless of whether or not the player is sprinting
	transform.Translate (Vector3.forward * VerticalTranslation * Time.deltaTime);
	
	//Move the player left and right. This only works if the player is not sprinting
	if(!isSprinting)
	{
		transform.Translate (Vector3.right * HorizontalTranslation * Time.deltaTime);
		
	}
	//Turn the player left and right
	transform.Rotate (Vector3.up * HorizontalRotation);
	//Rotate the camera up and down
	playerCamera.transform.Rotate (Vector3.left * VerticalRotation);
	
	if (Input.GetKey (KeyCode.LeftShift)) 
	{
		isSprinting = true;
	}
	else
	{
		isSprinting = false;
	}
	//Change movement and turning values based on sprinting bool
	if (isSprinting)
	{
		playerInfo.moveSpeed = 6;
		playerInfo.turnSpeed = 2;
	}
	else
	{
		playerInfo.moveSpeed = 3;
		playerInfo.turnSpeed = 4;
	}
	
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
	
	//Get the player's change in motion, since rigidbody.velocity is only yielding zero values
	velocity = transform.position - currentPosition;
	currentPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
	
}

	void IncreaseHit()
	{
		hits ++;
		if (hits > 2) 
		{
			Application.LoadLevel("Game_Over");
		}
		bloodAlpha.isActive = true;
		bloodAlpha.opacity = 1.0f;
		bloodAlpha.delay = 0.0f;
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

void OnCollisionExit(Collision other)
{
	touchingGround = false;
}

void OnCollisionEnter(Collision other)
{
	//Vector3 normal = other.contacts [0].normal;
	//Debug.Log(Vector3.Angle(Vector3.forward, normal));
	currentJumps = 0;
	touchingGround = true;
}

}
