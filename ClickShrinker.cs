using UnityEngine;
using System.Collections;

public class ClickShrinker : MonoBehaviour {
	private Vector3 originalScale;
	void Start () { originalScale = this.transform.localScale; }

	void OnMouseDown () {
		this.transform.localScale -= new Vector3 (.08f, .08f, 0);
	}

	void OnMouseUp () { transform.localScale = originalScale; }
}
