using UnityEngine;
using System.Collections;

public class Spectral_Hand : MonoBehaviour {

	public GameObject player;
	public Animator animState;

	void Start()
	{
		animState = transform.root.GetComponent<Animator> ();
	}

	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			if(transform.root.tag == "RedSpectral")
			{
				if(animState.GetCurrentAnimatorStateInfo(0).IsName("Left_Swipe"))
				{
					player.SendMessage("IncreaseHit");
				}
			}

			else if (transform.root.tag == "BlueSpectral")
			{
				if(animState.GetCurrentAnimatorStateInfo(0).IsName("Attack") || animState.GetCurrentAnimatorStateInfo(0).IsName("Attack_P2"))
				{
					player.SendMessage("IncreaseHit");
				}
			}
		}
	}


}
