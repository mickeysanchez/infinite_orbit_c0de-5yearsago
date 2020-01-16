using UnityEngine;
using System.Collections;

public class HandAnimator : MonoBehaviour {

	public Sprite[] Frames;
	private SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		GameManager.Notifications.AddListener (this, "PlayerUp");
		GameManager.Notifications.AddListener (this, "PlayerDown");
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void PlayerUp () {
		spriteRenderer.sprite = Frames[0];
	}

	void PlayerDown () {
		spriteRenderer.sprite = Frames[1];
	}
}
