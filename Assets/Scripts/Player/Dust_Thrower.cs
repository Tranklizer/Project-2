using UnityEngine;
using System.Collections;

public class Dust_Thrower : MonoBehaviour {

	public GameObject dust;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.E))
		{
			Object thrownDust = Instantiate(dust,transform.position,transform.rotation);
		}
	
	}
}
