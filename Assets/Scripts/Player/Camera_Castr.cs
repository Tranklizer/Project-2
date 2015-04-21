using UnityEngine;
using System.Collections;

public class Camera_Castr : MonoBehaviour {

	public Vincent_Move vincentMovement;

	// Use this for initialization
	void Start () {
		vincentMovement = GameObject.Find ("Vincent").GetComponent<Vincent_Move>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Debug.Log ("Clicked");
			
			Vector3 forward = transform.TransformDirection (Vector3.forward) * 10;
			
			RaycastHit hit;
			if(Physics.Raycast(transform.position, forward, out hit))
			{
				if(hit.collider.tag == "Environment")
				{
					vincentMovement.target.position = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
				}
				
			}
		}
	}
}
