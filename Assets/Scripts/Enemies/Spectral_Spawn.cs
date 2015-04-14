using UnityEngine;
using System.Collections;

public class Spectral_Spawn : MonoBehaviour {

	public GameObject Spectral;
	public GameObject Q;
	public Alpha_Animate QScript;
	bool activated = false;

	// Use this for initialization
	void Start () 
	{
		Spectral.SetActive (false);
		QScript = Q.GetComponent<Alpha_Animate> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && activated == false) 
		{
			activated = true;
			Spectral.SetActive(true);
			QScript.isActive = true;
			QScript.opacity = 1.0f;
			QScript.delay = 0.0f;

		}
	}
}
