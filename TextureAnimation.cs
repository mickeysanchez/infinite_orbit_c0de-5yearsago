using UnityEngine;
using System.Collections;

public class TextureAnimation : MonoBehaviour {
	//public bool IsLoop = false;
	//public float Duration = 5f;
	public float FramesPerSecond = 8f;
	public Texture2D [] animations;
	public GameObject MeshGameObject;
	
	private int itr = 0;
	private float lastTimeChanged = 0;
	
	void Update () 
	{
		//time check
		if (Time.time >= lastTimeChanged + (1/FramesPerSecond))
		{
			MeshGameObject.renderer.material.mainTexture = animations[itr];
			lastTimeChanged = Time.time;
			itr++;
			
			if (itr >= animations.Length)
			{
				itr = 0;
			}
		}
	}
}
