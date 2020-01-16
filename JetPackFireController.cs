using UnityEngine;
using System.Collections;

public class JetPackFireController : MonoBehaviour {

	private bool up;
	private Vector3 startingScale;
	private Vector3 startingPos;
	private float initialVolume;

	// Use this for initialization
	void Start () {
		GameManager.Notifications.AddListener (this, "PlayerUp");
		GameManager.Notifications.AddListener (this, "PlayerDown");
		GameManager.Notifications.AddListener (this, "OnGameplayStart");
		GameManager.Notifications.AddListener (this, "OnPlayerDeath");
		GameManager.Notifications.AddListener (this, "PreDeath");
		GameManager.Notifications.AddListener (this, "GamePaused");
		GameManager.Notifications.AddListener (this, "GameUnpaused");

		startingScale = transform.localScale;
		startingPos = transform.localPosition;

		initialVolume = audio.volume;
	}

	private bool paused = false;
	void GamePaused() {
		paused = true;
	}

	void GameUnpaused () {
		paused = false;
	}

	private bool dead = false;
	void PreDeath () {
		dead = true;
		up = false; 
		this.renderer.enabled = false;
		audio.volume = 0;
		audio.Stop ();
	}
	
	void OnPlayerDeath () {
		this.renderer.enabled = true;
		dead = true;
	}

	void OnGameplayStart () {
		this.renderer.enabled = true;
		dead = false;
	}

	private float audioSlant = .2f;
	void PlayerUp () 
	{
		if (!dead && !paused) {
			up = true;
			this.renderer.enabled = true;
			audio.volume = initialVolume;
			audioSlant = .2f;
			audio.Play ();
		}
	}

	void PlayerDown () 
	{
		up = false;
		this.renderer.enabled = false;
		audio.Stop ();
	}

	void Update () 
	{
		if (!dead) {
		if (up) 
			{
				transform.localScale += new Vector3(0, 0, 1 * Time.deltaTime);
//				transform.localPosition +=  new Vector3(0, -2 * Time.deltaTime, 0);
				audio.volume += audioSlant * Time.deltaTime;
			}
			else
			{
				transform.localScale = startingScale;
				transform.localPosition = startingPos;
			}
		}
	}
}
