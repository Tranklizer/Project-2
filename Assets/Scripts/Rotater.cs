using UnityEngine;
using System.Collections;

public class Rotater : MonoBehaviour {

	// Use this for initialization
	public float rotateSpeed;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (Vector3.up * rotateSpeed * Time.deltaTime);
	
	}
}
