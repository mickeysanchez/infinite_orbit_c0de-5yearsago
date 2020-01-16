using UnityEngine;
using System.Collections;
[RequireComponent (typeof (NotificationsManager))]

public class GameManager : MonoBehaviour {

	private bool paused = false;
	public float PowerUpSpeed = 2;
	public float PowerUpSpeedAcceleration = 0.05f;

	public static float LastScore;

	public static GameManager Instance
	{
		get
		{
			if(instance == null) instance = new GameObject ("GameManager").AddComponent<GameManager>();
			return instance;
		}
	}

	public static NotificationsManager Notifications
	{
		get
		{
			if(notifications == null) notifications = instance.GetComponent <NotificationsManager>();
			return notifications;
		}
	}

	private static GameManager instance = null;
	private static NotificationsManager notifications = null;

	void Awake ()
	{
		if ((instance) && (instance.GetInstanceID () != GetInstanceID ()))
			DestroyImmediate (gameObject);
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start ()
	{
		Notifications.AddListener (this, "GamePaused");
		Notifications.AddListener (this, "GameUnpaused");
		Notifications.AddListener (this, "PlayerUp");
		Notifications.AddListener (this, "PlayerDown");
		Notifications.AddListener (this, "PowerUp");
		Notifications.AddListener (this, "PowerDown");
		Notifications.AddListener (this, "OnPlayerDeath");
		Notifications.AddListener (this, "PowerUpEnding");
	}

	private bool powerUpEnding;
	void PowerUpEnding () {
		powerUpEnding = true;
	}

	void OnPlayerDeath () {
		Time.timeScale = 1f;
	}

	private bool poweredUp;
	void PowerUp () {
		powerUpEnding = false;
		poweredUp = true;
	}

	void PowerDown () {
		powerUpEnding = false;
		poweredUp = false;
	}

	private float savedPoweredUpTimeScale = 1f;

	void GamePaused ()
	{
		if (poweredUp)
			savedPoweredUpTimeScale = Time.timeScale;	
		Time.timeScale = 0;
		paused = true;
	}

	void GameUnpaused ()
	{
		if (poweredUp) {
			Time.timeScale = savedPoweredUpTimeScale;
		} else {
			Time.timeScale = 1;
		}
		paused = false;
	}

	void PlayerUp () {
		if (paused) {
			Time.timeScale = 0;
		} else {
			if (poweredUp) {
//				Time.timeScale = PowerUpSpeed;
			} else {
				Time.timeScale = 1.12f;	
			}
		}
	}

	void PlayerDown () { 
		if (paused) {
			Time.timeScale = 0;
		} else {
			if (poweredUp) {
//				Time.timeScale = PowerUpSpeed;
			} else {
				Time.timeScale = 1f;
			}
		}
	}

	void Update () {
		if (poweredUp && Time.timeScale < PowerUpSpeed) 
		{
			if (!powerUpEnding) 
			{
				Time.timeScale += PowerUpSpeedAcceleration*Time.deltaTime;
				if (Time.timeScale > PowerUpSpeed)
					Time.timeScale = PowerUpSpeed;
			} 
			else 
			{
				if (Time.timeScale > 1) 
				{
					Time.timeScale -= .07f*Time.deltaTime;
					if (Time.timeScale > 1)
						Time.timeScale = 1;
				}
			}
		}

	}

}
