using UnityEngine;
using System.Collections;

public class Dust_Controller : MonoBehaviour {

	Rigidbody rb;
	float thrust = 400.0f;

	float deathDelay = 0.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		Vector3 forward = transform.TransformDirection (Vector3.forward);
		rb.AddForce (forward * thrust);
	}
	
	// Update is called once per frame
	void Update () {

		deathDelay += Time.deltaTime;
		if (deathDelay > 5.0f) 
		{
			Destroy(this.gameObject);
		}
	
	}
}
