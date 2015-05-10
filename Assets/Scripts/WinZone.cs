using UnityEngine;
using System.Collections;

public class WinZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			Application.LoadLevel ("Main_Menu");
		}
	}
}
