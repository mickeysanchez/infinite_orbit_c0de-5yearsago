using UnityEngine;
using System.Collections;

public class MainMenuPlay : MonoBehaviour {
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
		StopAllAudio ();
		AutoFade.LoadLevel("gameplay",1,1,Color.black);
	}

	void Update () {
		if (stopAudio) {
			foreach(AudioSource audioS in allAudioSources) {
				audioS.volume -= Time.deltaTime;
			}
		}
	}
}
