using UnityEngine;
using System.Collections;

public class Alpha_Animate : MonoBehaviour {

	public float delay = 0.0f;
	public float opacity = 0.0f;
	public bool isActive = false;

	// Use this for initialization
	void Start () {
		renderer.material.color = new Color(1,1,1, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if (isActive && delay <=2.0f) 
		{
			opacity = 1.0f;
			renderer.material.color = new Color(1,1,1,opacity);
			delay += Time.deltaTime;
		}

		if (delay > 2.0f) 
		{
			delay += Time.deltaTime;
			opacity -= Time.deltaTime;
			renderer.material.color = new Color(1,1,1, opacity);
		}

		if (delay > 5.0f) 
		{
			delay = 0.0f;
			opacity = 0.0f;
			isActive = false;
		}
	
	}
}
