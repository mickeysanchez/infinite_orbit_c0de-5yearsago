using UnityEngine;
using System.Collections;

public class StreakController : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		transform.position -= new Vector3 (0, 0, 10f);
	}
}
