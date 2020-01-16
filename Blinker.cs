using UnityEngine;
using System.Collections;

public class Blinker : MonoBehaviour {
	public float TimeTil = 0.5f;
	private float counter = 0;
	private SpriteRenderer sr;

	void Start () {
		sr = GetComponent<SpriteRenderer>(); 
	}

	// Update is called once per frame
	void Update () {
		counter += Time.deltaTime;

		if (counter > TimeTil) {
			if (!sr.enabled) { 
				if (audio != null) {
					audio.Play (); 
				}
			}
			sr.enabled = (sr.enabled ? false : true);
			counter = 0;
		}
	}

	void OnEnable () {
		sr = GetComponent<SpriteRenderer>(); 
		sr.enabled = true;
		counter = 0;
	}
}
