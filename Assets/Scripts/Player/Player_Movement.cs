using UnityEngine;
using System.Collections;

public class Player_Movement : MonoBehaviour {

	public float movespeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			transform.Translate(Vector3.forward * movespeed * Time.deltaTime);
		}
	
	}
}
