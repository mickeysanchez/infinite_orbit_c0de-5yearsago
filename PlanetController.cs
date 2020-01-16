using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {

	public float rotationSpeed = -10f;
	public bool randRotation = false;
	private float randRotationX;
	private float randRotationY;
	private float randRotationZ;

	void Start () {
		if (randRotation) {
			randRotationX = Random.Range(-30f, 30f);
			randRotationY = Random.Range(-30f, 30f);
			randRotationZ = Random.Range(-30f, 30f);	
		}
	}

	// Update is called once per frame
	void Update () {
		if (randRotation) {
			transform.Rotate (randRotationX * Time.deltaTime, randRotationY * Time.deltaTime, randRotationZ * Time.deltaTime);
		} else {
			transform.Rotate (transform.up, -rotationSpeed * Time.deltaTime, Space.World);
		}
	}
}
