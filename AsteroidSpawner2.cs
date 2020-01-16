using UnityEngine;
using System.Collections;

public class AsteroidSpawner2 : MonoBehaviour {
	public GameObject asteroidPrefab;
	public GameObject fuelCannisterPrefab;
	
	public GameObject SpawnAsteroid(float pos) {
		GameObject asteroid;
		
		asteroid = (GameObject)Instantiate(asteroidPrefab, transform.position + new Vector3(0, pos, 0), Quaternion.identity);
		asteroid.transform.parent = transform.parent;
		
		return asteroid;
	}
	
	public void SpawnFuel(float pos) {
		GameObject fuelCannister;
		
		fuelCannister = (GameObject)Instantiate(fuelCannisterPrefab, transform.position + new Vector3(0, pos, 0), Quaternion.identity);
		fuelCannister.transform.parent = transform.parent;
	}
}