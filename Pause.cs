using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
	public Sprite PauseSprite;
	public Sprite PlaySprite;

	private bool paused = false;

	private bool afterFirstPress = false;

	void Start () 
	{
		GameManager.Notifications.AddListener (this, "GamePaused");
		GameManager.Notifications.AddListener (this, "GameUnpaused");
	}
	
	void OnMouseDown () 
	{
		if (paused) {
			GameManager.Notifications.PostNotification(this, "GameUnpaused");
		} else {
			GameManager.Notifications.PostNotification(this, "GamePaused");
		}
	}

	void GamePaused () { 
		paused = true;
		GetComponent<SpriteRenderer>().sprite = PlaySprite;
	}
	void GameUnpaused () { 
		paused = false;
		GetComponent<SpriteRenderer>().sprite = PauseSprite; 
	}
}
