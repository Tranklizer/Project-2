using UnityEngine;
using System.Collections;

public class Activate_RB : MonoBehaviour {
	

	// Use this for initialization
	void Start () {
		rigidbody.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "Player")
		{
			rigidbody.isKinematic = false;
		}
	}
}
