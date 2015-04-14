using UnityEngine;
using System.Collections;

public class Animated_Texture : MonoBehaviour {

	private Object[] objects; //Gets all the objects in the resources folder
	private Texture[] textures; //All the objects will be converted into textures
	private Material goMaterial;
	private int frameCounter = 0;
	bool animate = false;
	float frameDelay = 0.0f;

	void Awake()
	{
		//Get reference to the material of the gameObject this script is attached to
		this.goMaterial = this.renderer.material;
	}

	// Use this for initialization
	void Start () 
	{
		//Load all the textures;
		this.objects = Resources.LoadAll ("Sequence", typeof(Texture));

		this.textures = new Texture[objects.Length];

		for(int i = 0; i < objects.Length; i ++)
		{
			this.textures[i] = (Texture)this.objects[i];
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			animate = true;
		}

		if (animate) 
		{
			frameDelay += Time.deltaTime;
			if (frameDelay >= 0.04f)
			{
				frameDelay = 0.0f;
				frameCounter ++;
			}

			if (frameCounter == 12)
			{
				frameDelay = 0.0f;
				frameCounter = 0;
				animate = false;
			}
		}

		goMaterial.mainTexture = textures [frameCounter];
	
	}

	IEnumerator PlayLoop(float delay)
	{
		yield return new WaitForSeconds(delay);
		//Advance one frame
		frameCounter = (++frameCounter)%textures.Length;
		//Stop this Coroutine
		StopCoroutine("PlayLoop");
	}

	IEnumerator Play(float delay)
	{
		yield return new WaitForSeconds(delay);

		//If the frame counter isn't at the last frame
		if(frameCounter < textures.Length - 1)
		{
			++frameCounter;
		}

		StopCoroutine("PlayLoop");
	}
}
