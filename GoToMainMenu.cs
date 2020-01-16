using UnityEngine;
using System.Collections;

public class GoToMainMenu : MonoBehaviour {
	private AudioSource[] allAudioSources;
	private bool stopAudio;
	
	void Awake() {
		stopAudio = false;
		allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
	}
	
	void StopAllAudio() {
		stopAudio = true;
	}

	void OnMouseUp () 
	{
		GameManager.Notifications.PostNotification(this, "GameUnpaused");
		GameManager.Notifications.PostNotification(this, "FadeOutAudio");
		StopAllAudio ();
		AutoFade.LoadLevel("mainmenu",1,1,Color.black);
	}

	void Update () {
		if (stopAudio) {
			foreach(AudioSource audioS in allAudioSources) {
				audioS.volume -= Time.deltaTime;
			}
		}
	}
}
