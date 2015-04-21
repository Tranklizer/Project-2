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
		if (Vector3.Distance (transform.position, player.transform.position) <= 2 &&
			animState.GetCurrentAnimatorStateInfo (0).IsName ("Left_Swipe"))
		{
			Application.LoadLevel("Game_Over");
		}

	}


}
