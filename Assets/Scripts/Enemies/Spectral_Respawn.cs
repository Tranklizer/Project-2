using UnityEngine;
using System.Collections;

public class Spectral_Respawn : MonoBehaviour {
	public GameObject Spectral;
	public Transform respawnPoint;
	public Spectral_Anim targetPoints; //The wander points of the current Spectral
	public Spectral_Anim setPoints; //The wander points of the spawned Spectral

	// Use this for initialization
	void Start () {

		respawnPoint = GameObject.Find ("Spectral_Respawn").transform;
		targetPoints = gameObject.GetComponent<Spectral_Anim> ();
		setPoints = Spectral.GetComponent<Spectral_Anim> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RespawnSpectral()
	{
		setPoints.wanderPoints = new Transform[targetPoints.wanderPoints.Length];
		for (int waypoint = 0; waypoint < targetPoints.wanderPoints.Length; waypoint ++)
		{
			setPoints.wanderPoints[waypoint] = targetPoints.wanderPoints[waypoint];
		}
		Instantiate(Spectral,respawnPoint.position,Quaternion.identity);
		this.gameObject.SetActive (false);

	}


}
