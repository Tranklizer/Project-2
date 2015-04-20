using UnityEngine;
using System.Collections;

public class Dust_Tester : MonoBehaviour {

	public Material spectralReflect;
	public Material normalVision;

	public Texture invisible;
	public Texture dust;

	bool hasDust = false;

	public Change_Vision cameraSettings;

	// Use this for initialization
	void Start () 
	{
		cameraSettings = GameObject.Find ("Main Camera").GetComponent<Change_Vision> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (cameraSettings.normalVision) 
		{
			renderer.material = normalVision;
		}

		else
		{
			renderer.material = spectralReflect;
		}

		if(hasDust && cameraSettings.normalVision)
		{
			renderer.material.mainTexture = dust;
		}
		else if (cameraSettings.normalVision && !hasDust)
		{
			renderer.material.mainTexture = invisible;
		}
	}

	void OnParticleCollision(GameObject other)
	{
		if (!hasDust) 
		{
			hasDust = true;
			renderer.material.mainTexture = dust;
		}
	}
	
}
