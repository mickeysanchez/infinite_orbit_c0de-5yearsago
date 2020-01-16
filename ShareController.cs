using UnityEngine;
using System.Collections;

public class ShareController : MonoBehaviour {
	private const string FACEBOOK_APP_ID = "311150745743299";

	void Awake () 
	{
		FB.Init(SetInit, OnHideUnity);
	}
	
	private void SetInit() {
		enabled = true; 
		// "enabled" is a magic global; this lets us wait for FB before we start rendering
	}
	
	private void OnHideUnity(bool isGameShown) {
		if (!isGameShown) {
			// pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// start the game back up - we're getting focus again
			Time.timeScale = 1;
		}
	}

	void AuthCallback(FBResult result) {
		if(FB.IsLoggedIn) {
			Feed ();
		} else {
			Debug.Log("User cancelled login");
		}
	}

	void Feed ()
	{
		float lastScore = GameManager.LastScore;

		int minutes = (int)lastScore / 60;
		int seconds = (int)lastScore % 60;
		int fraction = (int)(lastScore * 100) % 100;
		string lastDisplayTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);

		FB.Feed(
			link: "http://newbeings.com",
			linkName: "Space Escape",
			linkCaption: "Space Escape",
			linkDescription: "I just flew through space for " + lastDisplayTime + ". Think you can beat me?",
			picture: "http://newbeings.com/spaceman.png",
			callback: LogCallback
		);
	}

	void LogCallback(FBResult response) {
		Debug.Log(response.Text);
	}

	void OnMouseUp () {
		if (!FB.IsLoggedIn) 
		{
			FB.Login("email, publish_actions", AuthCallback);
		} else
		{
			Feed ();
		}
	}


	
}
