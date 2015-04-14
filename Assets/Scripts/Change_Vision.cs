using UnityEngine;
using System.Collections;

public class Change_Vision : MonoBehaviour {

	public bool normalVision = true;
	LayerMask normal = 256;
	LayerMask spectral = 512;

	// Use this for initialization
	void Start () {
		camera.cullingMask = ~(1 << 9);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			if (normalVision)
			{
				camera.cullingMask = 1 << 9 | 1 << 10;
				normalVision = false;
			}
			else
			{
				camera.cullingMask = 1 << 8 | 1 << 10;
				normalVision = true;
			}
		}
	
	}
}
