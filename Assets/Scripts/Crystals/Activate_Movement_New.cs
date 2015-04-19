using UnityEngine;
using System.Collections;

public class Activate_Movement_New : MonoBehaviour {

	public bool activateRigid;
	public bool activateAnim;
	public GameObject[] RigidBodies = new GameObject[1];
	public GameObject[] Animations = new GameObject[1];
	public bool activated = false;
	public int animationIndex = 0;
	public float delayTimer = 0.0f;
	float rigidDelay = 0f;
	public GameObject particles;

	public GameObject explosionPosition;

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
				animator.GetComponentInChildren<BoxCollider>().enabled = false;
				animator.GetComponentInChildren<Animator>().enabled = false;
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		if(activated && animationIndex < Animations.Length)
		{
			delayTimer += Time.deltaTime;
			if (delayTimer >= 0.15f)
			{
				delayTimer = 0.0f;
				Animations[animationIndex].GetComponentInChildren<Animator>().enabled = true;
				Animations[animationIndex].GetComponentInChildren<BoxCollider>().enabled = true;
				animationIndex++;
			}
			
		}

		if (activated && rigidDelay <= 2.0f) 
		{
			rigidDelay += Time.deltaTime;
		}
		if (rigidDelay > 2.0f) 
		{
			rigidDelay += Time.deltaTime;
			foreach(GameObject rigid in RigidBodies)
			{
				rigid.GetComponent<BoxCollider>().enabled = false;
			}
		}
		if (rigidDelay > 4.0f) 
		{
			foreach(GameObject rigid in RigidBodies)
			{
				rigid.SetActive(false);
			}
		}
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			if(activated != true)
			{
			activated = true;
			Instantiate(particles,transform.position,Quaternion.identity);
			}

			if (activateRigid) 
			{
				foreach (GameObject rigid in RigidBodies) 
				{
					rigid.rigidbody.isKinematic = false;
					rigid.rigidbody.AddExplosionForce(100,explosionPosition.transform.position,4);
				}
			}
		}
	}
}
