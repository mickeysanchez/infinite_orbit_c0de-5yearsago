using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class GUICam : MonoBehaviour {

	private Camera Cam = null;

	public float PixelToWorldScale;

	private Transform ThisTransform = null;

	void Start () {
		Cam = GetComponent<Camera> ();
		ThisTransform = transform;

		PixelToWorldScale = Screen.width / 11;
	}

	void Update () {
		Cam.orthographicSize = Screen.height / 2 / PixelToWorldScale;
		ThisTransform.localPosition = new Vector3 (Screen.width / 2 / PixelToWorldScale, -(Screen.height / 2 / PixelToWorldScale), ThisTransform.localPosition.z);
	}
}
