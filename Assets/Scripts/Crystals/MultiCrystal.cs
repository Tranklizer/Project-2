﻿using UnityEngine;
using System.Collections;

public class MultiCrystal : MonoBehaviour {

	public GameObject[] Crystals = new GameObject[3];
	public Activate_Movement_New[] crystalActivations = new Activate_Movement_New[3];
	public GameObject[] Animations = new GameObject[1];
	bool allActive;

	// Use this for initialization
	void Start () {
		int currentCrystal = 0;
		foreach (GameObject crystal in Crystals) 
		{
			crystalActivations[currentCrystal] = crystal.GetComponent<Activate_Movement_New>();
			currentCrystal ++;
		}

		foreach (GameObject animator in Animations)
		{

			animator.GetComponent<Animator>().enabled = false;
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < 3; i++) 
		{
			if (crystalActivations[i].activated == true)
			{
				allActive = true;
			}
			else
			{
				allActive = false;
				break;
			}
		}

		if (allActive) 
		{
			foreach (GameObject animator in Animations)
			{
				animator.GetComponent<Animator>().enabled = true;
			}
		}
	
	}
}