using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {
	public float velocity = -55.94f;
	private float randSpeed;
	private float randRotationX;
	private float randRotationY;
	private float randRotationZ;

	public bool ShouldRotate = false;
	void Start () {
		randSpeed = Random.Range(-1.5f, 1.5f);
		randRotationX = Random.Range(-30f, 30f);
		randRotationY = Random.Range(-30f, 30f);
		randRotationZ = Random.Range(-30f, 30f);	
	}

	void Update () {

//		this.renderer.material.color = new Color();
		transform.position += new Vector3(0,0, (velocity + randSpeed) * Time.deltaTime);
		if (ShouldRotate) 
			transform.Rotate(randRotationX * Time.deltaTime, randRotationY * Time.deltaTime, randRotationZ * Time.deltaTime);
	}
}