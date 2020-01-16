using UnityEngine;
using System.Collections;

public class GamePlaySceneManager : MonoBehaviour {
	private AudioSource[] allAudioSources;
	public static bool fadeAudio;

	// Use this for initialization
	void Awake () {
		fadeAudio = false;
		allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
	}

	void Start () {
		GameManager.Notifications.AddListener (this, "FadeOutAudio");
	}

	void FadeOutAudio () {
		fadeAudio = true;
	}

	// Update is called once per frame
	void Update () {
		if (fadeAudio) {
			foreach(AudioSource audioS in allAudioSources) {
				audioS.volume -= Time.deltaTime*3;
			}
		}
	}
}
