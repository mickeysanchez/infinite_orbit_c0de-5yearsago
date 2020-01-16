using UnityEngine;
using System.Collections;

public class WarningIndicatorController : MonoBehaviour {
	public GameObject WarningIndicator;
	public float DeathTime = 1;
	private bool warning;
	private float counter = 0;
	public GUIStyle myStyle;

	// Use this for initialization
	void Start () {
		WarningIndicator.SetActive (false);
		GameManager.Notifications.AddListener (this, "InitiateWarning");
		GameManager.Notifications.AddListener (this, "EndWarning");
	}

	void InitiateWarning () {
		WarningIndicator.SetActive (true);
		warning = true;
	}

	void EndWarning () {
		WarningIndicator.SetActive (false);
		warning = false;
		counter = 0;
	}

	void Update () {
		if (warning) { 
			counter += Time.deltaTime;
		}

		if (counter > DeathTime+.2f) {
			counter = 0;
			warning = false;
			GameManager.Notifications.PostNotification(this, "PreDeath");
			GameManager.Notifications.PostNotification(this, "OnPlayerDeath");
		}
	}

	void OnGUI () {
		if (warning) {
			int time = (int)Mathf.Round ( (DeathTime-counter) / (DeathTime/3) );
			GUI.Box ( new Rect (Screen.width/2 - 101, Screen.height/2.4f, 200, 25), time.ToString (), myStyle);
		}
	}
}
