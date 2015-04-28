using UnityEngine;
using System.Collections;

public class Change_Vision : MonoBehaviour {

	public bool normalVision = true;
	public bool spectralVision = false;
	public bool blueVision = false;

	//LayerMask normal = 256;
	//LayerMask spectral = 512;

	// Use this for initialization
	void Start () {
		camera.cullingMask = ~(1 << 9);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if (normalVision || blueVision)
			{
				camera.cullingMask = 1 << 9 | 1 << 10;
				normalVision = false;
				blueVision = false;
				spectralVision = true;
			}
			else
			{
				camera.cullingMask = 1 << 8 | 1 << 10;
				normalVision = true;
				spectralVision = false;
				blueVision = false;
			}
		}

		if (Input.GetKeyDown (KeyCode.E))
		{
			if (normalVision || spectralVision)
			{
				camera.cullingMask =  1 << 10 | 1 << 11;
				blueVision = true;
				normalVision = false;
				spectralVision = false;
			}

			else
			{
				camera.cullingMask = 1 << 8 | 1 << 10;
				normalVision = true;
				spectralVision = false;
				blueVision = false;
			}
		}
	
	}
}
