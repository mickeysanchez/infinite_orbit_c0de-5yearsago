using UnityEngine;
using System.Collections;

public class AsteroidDestroyer : MonoBehaviour {
	public AsteroidManager AsteroidManager;

	void OnTriggerEnter (Collider other) {
		Destroy (other.gameObject);
	}
}