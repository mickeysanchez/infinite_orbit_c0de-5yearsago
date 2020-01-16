using UnityEngine;
using System.Collections;

public class PlayAgain : MonoBehaviour {
	private bool buttonClicked = false;

	void OnMouseDown () 
	{
		buttonClicked = true;
//		GameManager.Notifications.PostNotification (this, "OnGameplayStart");
	}

	void OnMouseUp ()
	{
		GameManager.Notifications.PostNotification (this, "PlayerReset");
		GameManager.Notifications.PostNotification(this, "GameUnpaused");
		GameManager.Notifications.PostNotification (this, "OnGameplayStart");
	}
}
