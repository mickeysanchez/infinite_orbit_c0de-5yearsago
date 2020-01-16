using UnityEngine;
using System.Collections;

public class JetpackController : MonoBehaviour {

	public Texture2D[] fireFrames;
	public int FramesPerSecond = 8;
	private int currentIndex = 0;
	private int lastIndex = -1;

	// Update is called once per frame
	void Update () {
		currentIndex = (int)(Mathf.Repeat (Time.time*FramesPerSecond, fireFrames.Length));

		if (currentIndex != lastIndex) 
	    {
			this.renderer.material.mainTexture = fireFrames[currentIndex];
			currentIndex = lastIndex;
		}
		
	}
}
