using UnityEngine;
using System.Collections;

public class UpAndDowner : MonoBehaviour {

	private int toggler;
	public float vel;
	public float distance;
	private float speed;
	private float initialY;

	void Start () {
		toggler = Random.Range (-1, 1);
		vel = Random.Range (.1f, .3f);
		initialY = transform.position.y;
	}

	void Update () {

		if ( toggler >= 0 ) { 
			speed += vel;
			if (transform.position.y > initialY && Mathf.Abs (initialY-transform.position.y) > distance/2) {
				toggler = -1;
			}
		} else {
			speed-=vel;

			if (transform.position.y < initialY && Mathf.Abs (initialY-transform.position.y) > distance/2) {
				toggler = 1;
			}
		}


		transform.position += new Vector3 (0, Time.deltaTime*speed, 0);	
	} 
}
