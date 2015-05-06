using UnityEngine;
using System.Collections;

public class Activate_RB : MonoBehaviour {

	float collisionDelay = 0.0f;
	bool activated = false;

	// Use this for initialization
	void Start () {
		rigidbody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(activated)
		{
			collisionDelay += Time.deltaTime;
		}
		if(collisionDelay > 5.0f)
		{
			gameObject.GetComponent<BoxCollider>().enabled = false;
		}

		if(collisionDelay > 8.0f)
		{
			Destroy(this.gameObject);
		}
	
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			rigidbody.isKinematic = false;
			activated = true;
		}
	}
}
