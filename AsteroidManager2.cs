using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager2 : MonoBehaviour {
	public AsteroidSpawner2 AsteroidSpawner;
	public Queue<GameObject> Asteroids;
	public int AsteroidsCount;
	public int MaxAsteroidCount = 4;
	public float AsteroidWaitTime = 1;
	private float asteroidWaitTimeCounter = 0;
	
	public Transform pivot;
	
	private bool asteroidsShouldBeGoing = false;
	
	public void SpawnAsteroid() {
		//		float randomUpDown = Random.Range (-AsteroidSpawner.transform.localScale.y, AsteroidSpawner.transform.localScale.y);
		
		float oneOfThree = Random.Range (0, 6);
		
		if (oneOfThree == 0) 
		{
			// Middle Opening
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*1));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*1));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3));
			AsteroidsCount += 6;
		}
		else if (oneOfThree == 1)
		{
			// Bottom Opening
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (0.0f));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*4));
			AsteroidsCount += 5;
		}
		else if (oneOfThree == 2)
		{
			// Top Opening
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*4));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (0.0f));
			AsteroidsCount += 5;
		}
		else if (oneOfThree == 3)
		{
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*1));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3));
			AsteroidsCount += 6;
		}
		else if (oneOfThree == 4) 
		{
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*1));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*0));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3));
			AsteroidsCount += 6;
		} 
		else 
		{
			//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*.5f));
			//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*0));
			//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*.5f));
			//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2));
			//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3));
			AsteroidsCount += 1;
		}
		
	}
	
	public void StopAsteroids() {
		
		for (int i = 0; i < AsteroidsCount; i++) {
			GameObject a = Asteroids.Dequeue ();
			Destroy (a);
		}
		AsteroidsCount = 0;
	}
	
	void Start () {
		Asteroids = new Queue<GameObject> ();
		
		GameManager.Notifications.AddListener (this, "OnPlayerDeath");
		GameManager.Notifications.AddListener (this, "AfterFirstPress");
		GameManager.Notifications.AddListener (this, "PlayerReset");
	}
	
	void PlayerReset ()
	{
		StopAsteroids ();
	}
	
	void AfterFirstPress () {
		asteroidsShouldBeGoing = true;
	}
	
	void OnPlayerDeath() {
		asteroidsShouldBeGoing = false;
		StopAsteroids ();
	}
	
	private int[] fuel = {0,0,0,1};
	private int fuelCounter = 0;
	void Update () {
		if (asteroidsShouldBeGoing) {
			
			if (asteroidWaitTimeCounter < AsteroidWaitTime) {
				asteroidWaitTimeCounter += (1 * Time.deltaTime);
			} else {
				SpawnAsteroid ();
				asteroidWaitTimeCounter = 0;
				fuelCounter += 1;
			}
			
			if (asteroidWaitTimeCounter > AsteroidWaitTime/2 && fuel[fuelCounter] == 1) {
				float oneOfThree = Random.Range (0, 2);
				float zeroOne = Random.Range(0,2);
				if (zeroOne >= 1) { oneOfThree = -oneOfThree; }
				AsteroidSpawner.SpawnFuel(AsteroidSpawner.transform.position.y*oneOfThree);
				
				fuelCounter = 0;
			}
			
//			foreach (GameObject asteroid in Asteroids) {
//				if (asteroid != null) 
//					asteroid.transform.RotateAround(pivot.position, pivot.up, -20 * Time.deltaTime);
//			}
			
		}
	}
}


