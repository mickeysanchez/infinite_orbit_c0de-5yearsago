using UnityEngine;
using System.Collections;

public class AsteroidSpawner : MonoBehaviour {
	public GameObject asteroidPrefab2;
	public GameObject fuelCannisterPrefab;
	public GameObject PowerUp;
	public GameObject Shield;

	public GameObject SpawnAsteroid(float pos, GameObject asteroidPrefab) {
		GameObject asteroid;

		float randX = Random.Range(-0.3f, .3f);

		asteroid = (GameObject)Instantiate(asteroidPrefab2, transform.position + new Vector3(randX, pos, 0), Quaternion.identity);
		asteroid.transform.parent = transform.parent;
		
		return asteroid;
	}

	public GameObject SpawnFuel(float pos) {
		GameObject fuelCannister;
		
		fuelCannister = (GameObject)Instantiate(fuelCannisterPrefab, transform.position + new Vector3(0, pos, 0), Quaternion.identity);
		fuelCannister.transform.parent = transform.parent;

		return fuelCannister;
	}

	public GameObject SpawnPowerUp(float pos) {
		GameObject powerUp;
		
		powerUp = (GameObject)Instantiate(PowerUp, transform.position + new Vector3(0, pos, 0), Quaternion.identity);
		powerUp.transform.parent = transform.parent;
		
		return powerUp;
	}

	public GameObject SpawnShield(float pos) {
		GameObject shield;
		
		shield = (GameObject)Instantiate(Shield, transform.position + new Vector3(0, pos, 0), Quaternion.identity);
		shield.transform.parent = transform.parent;
		
		return shield;
	}
}