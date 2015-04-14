using UnityEngine;
using System.Collections;

public class Player_Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		Debug.Log ("Hit");
		if (other.gameObject.tag == "Spectral") 
		{
			Debug.Log("Player is Dead");
		}
	}
}
