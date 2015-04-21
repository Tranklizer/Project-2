using UnityEngine;
using System.Collections;

public class Dust_Tester : MonoBehaviour {

	public Material spectralReflect;
	public Material normalVision;

	public Texture invisible;
	public Texture dust;

	bool hasDust = false;

	float opacity = 0.0f;
	bool maxDust = false;

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
			//Make the dust fade in and out
			if(!maxDust)
			{
				opacity += Time.deltaTime;
				if (opacity >= 1.0f)
				{
					maxDust = true;
				}
			}
			else
			{
				opacity -= (Time.deltaTime / 10);
			}
			renderer.material.mainTexture = dust;
			renderer.material.color = new Color(1,1,1,opacity);


			if (opacity < 0.0f)
			{
				opacity = 0.0f;
				hasDust = false;
				maxDust = false;
			}

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
			renderer.material.color = new Color(1,1,1,0);
		}
	}
	
}
