using UnityEngine;
using System.Collections;

public class Activate_Movement : MonoBehaviour {

	//Take an array of gameObjects and activate their rigidBody components
	public bool activateRigid;
	public bool activateAnim;
	public GameObject[] RigidBodies = new GameObject[1];
	public GameObject[] Animations = new GameObject[1];
	public bool activated = false;
	//Rigidbody[] debrisRigid = new Rigidbody[5];
	
	// Use this for initialization
	void Start () {
		if (activateRigid) {
			foreach (GameObject rigid in RigidBodies) 
			{
				rigid.rigidbody.isKinematic = true;
			}
		}
		
		if (activateAnim) 
		{
			foreach (GameObject animator in Animations)
			{
				animator.GetComponent<Animator>().enabled = false;
			}
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			activated = true;
			if (activateRigid) 
			{
				foreach (GameObject rigid in RigidBodies) 
				{
					rigid.rigidbody.isKinematic = false;
				}
			}
			
			if (activateAnim)
			{
				foreach (GameObject animator in Animations)
				{
					animator.GetComponent<Animator>().enabled = true;
				}
			}
		}
	}
}
