using UnityEngine;
using System.Collections;

public class MainMenuPlayer : MonoBehaviour {
	public float upSpeedLimit = 6.0f;
	public float upSpeed = 0.5f;
	public float downSpeed = 0.5f;
	
	private float velocity = 0.0f;
	private bool movementAllowed = false;
	private bool beforeFirstPress = true;
	private bool gamePlayStarted = true;
	
	// HOVER ANIMATION
	private bool hoverTrigger = true;
	private float hoverTiming = 0;
	
	void hoveringAnimation () {
		if (hoverTrigger) {
			velocity = 0.5f;
		} else {
			velocity = -0.5f;
		}
		
		transform.position = transform.position + new Vector3(0, velocity * Time.deltaTime, 0);
		
		if (hoverTiming < 80) {
			hoverTiming += 1;
		} else {
			hoverTiming = 0;
			hoverTrigger = hoverTrigger ? false : true;
		}
	}
	
	void Update () {
		hoveringAnimation();
	}
}