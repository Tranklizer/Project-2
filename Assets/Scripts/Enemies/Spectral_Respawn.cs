using UnityEngine;
using System.Collections;

public class Spectral_Respawn : MonoBehaviour {
	public GameObject Spectral;

	// Use this for initialization
	void Start () {

		Spectral.SetActive (false);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == "BlueSpectral")
		{
			Spectral.SetActive(true);
		}
	}
}
