using UnityEngine;
using System.Collections;

public class GUIAnimator : MonoBehaviour {

	public Sprite[] Frames;
	public int FramesPerSecond = 8;
	private int currentIndex = 0;
	private int lastIndex = -1;
	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		currentIndex = (int)(Mathf.Repeat (Time.time*FramesPerSecond, Frames.Length));

		if (currentIndex != lastIndex) 
		{
			spriteRenderer.sprite = Frames[currentIndex];
			currentIndex = lastIndex;
		}
		
	}
}
