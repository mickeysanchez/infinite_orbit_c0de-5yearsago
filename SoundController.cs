using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	public Sprite SoundOnSprite;
	public Sprite SoundOffSprite;
	public GameObject GM;
	private AudioListener audioListener;
	public GameObject MainMenuMusic;
	public GameObject GameplayMusic;

	private bool sound = true;

	private AudioSource[] allAudioSources;
	
	void StopAllAudio() {
		foreach(AudioSource audioS in allAudioSources) {
			audioS.Stop();
		}
	}

	
	void ReturnAllAudio() {
		foreach(AudioSource audioS in allAudioSources) {
			audioS.volume = 1;
		}
	}

	void OnLevelWasLoaded (int level) {

	}

	void Start () 
	{
		allAudioSources = FindObjectsOfType<AudioSource>() as AudioSource[];
		StopAllAudio ();
		ReturnAllAudio ();

		audioListener = GM.GetComponent<AudioListener> ();
		
		if ( PlayerPrefs.HasKey("sound") )
		{
			string soundString = PlayerPrefs.GetString("sound");
			
			if (soundString == "on") 
			{
				sound = true;
				AudioListener.pause = false;
				if (Application.loadedLevelName == "mainmenu") {
					MainMenuMusic.audio.Play ();
				} else {
					GameplayMusic.audio.Play ();
				}
				
				
				if (SoundOnSprite != null) {
					GetComponent<SpriteRenderer>().sprite = SoundOnSprite;
				}
			}
			else
			{
				sound = false;
				AudioListener.pause = true;
				if (SoundOffSprite != null) {
					GetComponent<SpriteRenderer>().sprite = SoundOffSprite;
				}
			}
		}
		else 
		{
			sound = true;
			PlayerPrefs.SetString("sound", "on");
			AudioListener.pause = false;
			if (Application.loadedLevelName == "mainmenu") {
				MainMenuMusic.audio.Play ();
			} else {
				GameplayMusic.audio.Play ();
			}
		}
	}
	
	void OnMouseUp () 
	{
		if (sound) {
			sound = false;
			PlayerPrefs.SetString("sound", "off");

			AudioListener.pause = true;

			if (SoundOffSprite != null) {
				GetComponent<SpriteRenderer>().sprite = SoundOffSprite;
			}
		} else {
			sound = true;
			PlayerPrefs.SetString("sound", "on");
			AudioListener.pause = false;

			if (Application.loadedLevelName == "mainmenu") {
				MainMenuMusic.audio.Play ();
			} else {
				GameplayMusic.audio.Play ();
			}

			if (SoundOnSprite != null) {
				GetComponent<SpriteRenderer>().sprite = SoundOnSprite;
			}
		}
	}

}
