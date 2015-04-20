using UnityEngine;
using System.Collections;

public class Particle_Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other)
	{
		Debug.Log ("Hit by Particle");
	}
}
