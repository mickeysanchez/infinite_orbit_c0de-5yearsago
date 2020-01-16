using UnityEngine;
using System.Collections;

public class MainCameraController : MonoBehaviour {

	public float RightMovementVel = 1;
	public float BackMovementVel = 1;

	public float RightThreshold = 1;
	public float BackThreshold = 1;

	public float FieldOfViewThreshold = 80;
	public float FieldOfViewVel = 10;

	private Vector3 initialPosition;
	private float initialFieldOfView;

	private bool playerUp = false;

	// Use this for initialization
	void Start () {
		GameManager.Notifications.AddListener (this, "PlayerUp");
		GameManager.Notifications.AddListener (this, "PlayerDown");

//		if ((float)Screen.width / Screen.height < .6f) {
//			transform.localPosition += new Vector3 (((float)Screen.width / Screen.height) * 4, 0, -((float)Screen.width / Screen.height) * 9);
//		}

		RightThreshold = this.transform.position.x - RightThreshold;
		BackThreshold = this.transform.position.z - BackThreshold;

		initialPosition = this.transform.position;
		initialFieldOfView = camera.fieldOfView;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerUp) {
			if (transform.position.x > RightThreshold) {
				transform.localPosition += new Vector3(-RightMovementVel * Time.deltaTime, 0, 0);
			}

			if (transform.position.z > BackThreshold) {
				transform.localPosition += new Vector3(0, 0, -BackMovementVel * Time.deltaTime);
			}

			if (camera.fieldOfView < FieldOfViewThreshold) { camera.fieldOfView += FieldOfViewVel*Time.deltaTime; }
		} else {
			if (transform.position.x < initialPosition.x) {
				transform.localPosition += new Vector3(RightMovementVel * Time.deltaTime, 0, 0);
			}
			
			if (transform.position.z < initialPosition.z) {
				transform.localPosition += new Vector3(0, 0, BackMovementVel * Time.deltaTime);
			}

			if (camera.fieldOfView > initialFieldOfView) { camera.fieldOfView -= FieldOfViewVel*Time.deltaTime; }
		}
	}

	void PlayerUp () {
		playerUp = true;
	}

	void PlayerDown () {
		playerUp = false;
	}
}
