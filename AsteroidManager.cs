using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidManager : MonoBehaviour {
	public GameObject[] AsteroidPrefabs;
	public AsteroidSpawner AsteroidSpawner;
	public Queue<GameObject> Asteroids;
	public Queue<GameObject> Fuels;
	public Queue<GameObject> PowerUps;
	public Queue<GameObject> Shields;
	public int AsteroidsCount;
	public int FuelsCounter;
	public int PowerUpsCounter;
	public int ShieldsCounter;
	public int MaxAsteroidCount = 4;
	public float AsteroidWaitTime = 1;
	private float asteroidWaitTimeCounter = 0;

	public bool MainMenu = false;

//	public Transform pivot;

	private bool asteroidsShouldBeGoing = false;

	public void SpawnAsteroid() {
//		float randomUpDown = Random.Range (-AsteroidSpawner.transform.localScale.y, AsteroidSpawner.transform.localScale.y);

		float oneOfThree = Random.Range (0, 6);

		if (oneOfThree == 0) 
		{
			// Middle Opening
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*1, AsteroidPrefabs[5]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[6]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[7]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*1, AsteroidPrefabs[3]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[2]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[1]));
			AsteroidsCount += 6;
		}
		else if (oneOfThree == 1)
		{
			// Bottom Opening
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (0.0f, AsteroidPrefabs[4]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y, AsteroidPrefabs[3]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[2]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[1]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*4, AsteroidPrefabs[0]));
			AsteroidsCount += 5;
		}
		else if (oneOfThree == 2)
		{
			// Top Opening
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y, AsteroidPrefabs[5]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[6]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[7]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*4, AsteroidPrefabs[8]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (0.0f, AsteroidPrefabs[4]));
			AsteroidsCount += 5;
		}
		else if (oneOfThree == 3)
		{
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y, AsteroidPrefabs[5]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[6]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[7]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*1, AsteroidPrefabs[3]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[2]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[1]));
			AsteroidsCount += 6;
		}
		else if (oneOfThree == 4) 
		{
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*1, AsteroidPrefabs[5]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[6]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[7]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*0, AsteroidPrefabs[4]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*2, AsteroidPrefabs[3]));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (-AsteroidSpawner.transform.position.y*3, AsteroidPrefabs[2]));
			AsteroidsCount += 6;
		} 
		else 
		{
//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*.5f));
//			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*2));
			Asteroids.Enqueue (AsteroidSpawner.SpawnAsteroid (AsteroidSpawner.transform.position.y*0, AsteroidPrefabs[4]));
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

		for (int i = 0; i < FuelsCounter; i++) {
			GameObject a = Fuels.Dequeue ();
			Destroy (a);
		}
		FuelsCounter = 0;

		for (int i = 0; i < PowerUpsCounter; i++) {
			GameObject a = PowerUps.Dequeue ();
			Destroy (a);
		}
		PowerUpsCounter = 0;

		for (int i = 0; i < ShieldsCounter; i++) {
			GameObject a = Shields.Dequeue ();
			Destroy (a);
		}
		ShieldsCounter = 0;
	}
	
	void Start () {
		Asteroids = new Queue<GameObject> ();
		Fuels = new Queue<GameObject> ();
		PowerUps = new Queue<GameObject> ();
		Shields = new Queue<GameObject> ();

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
		fuelCounter = 0;

		// powerUp doesn't come unless you last.
		powerUpCounter = 0;
	}

	private int[] fuel = {0,0,0,1};
	private int fuelCounter = 0;
	private int[] powerUpSpots = {0,0,0,0,0,0,0,0,0,0,0,1};
	private int powerUpCounter = 0;
	private int[] shieldSpots = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};
	private int shieldCounter = 0;
	
	void Update () {
		if (asteroidsShouldBeGoing) {

			if (asteroidWaitTimeCounter < AsteroidWaitTime) {
				asteroidWaitTimeCounter += (1 * Time.deltaTime);
			} else {
				SpawnAsteroid ();
				asteroidWaitTimeCounter = 0;
				fuelCounter += 1;
				powerUpCounter += 1;
				shieldCounter += 1;
			}

			if (asteroidWaitTimeCounter > AsteroidWaitTime/2 && powerUpSpots[powerUpCounter] == 1) {
				if (fuel[fuelCounter] == 1) {
					powerUpCounter = 0;
				} else {
					float oneOfThree = Random.Range (0, 2);
					float zeroOne = Random.Range(0,2);
					if (zeroOne >= 1) { oneOfThree = -oneOfThree; }
					
					PowerUps.Enqueue(AsteroidSpawner.SpawnPowerUp(AsteroidSpawner.transform.position.y*oneOfThree));
					PowerUpsCounter += 1;
					
					powerUpCounter = 0;
				}
			}

			if (asteroidWaitTimeCounter > AsteroidWaitTime/2 && shieldSpots[shieldCounter] == 1) {
				if (fuel[fuelCounter] == 1) 
				{
					shieldCounter = 0;
				}
				else
				{
					float oneOfThree = Random.Range (0, 2);
					float zeroOne = Random.Range(0,2);
					if (zeroOne >= 1) { oneOfThree = -oneOfThree; }
					
					Shields.Enqueue(AsteroidSpawner.SpawnShield(AsteroidSpawner.transform.position.y*oneOfThree));
					ShieldsCounter +=1;
					
					shieldCounter = 0;
				}
			}

			if (asteroidWaitTimeCounter > AsteroidWaitTime/2 && fuel[fuelCounter] == 1) {
				float oneOfThree = Random.Range (0, 2);
				float zeroOne = Random.Range(0,2);
				if (zeroOne >= 1) { oneOfThree = -oneOfThree; }

				Fuels.Enqueue(AsteroidSpawner.SpawnFuel(AsteroidSpawner.transform.position.y*oneOfThree));
				FuelsCounter +=1;

				fuelCounter = 0;
			}


		}

		if (MainMenu) 
		{
			if (asteroidWaitTimeCounter < AsteroidWaitTime) {
				asteroidWaitTimeCounter += (1 * Time.deltaTime);
			} else {
				float randomUpDown = Random.Range (-AsteroidSpawner.transform.localScale.y, AsteroidSpawner.transform.localScale.y);
				AsteroidSpawner.SpawnAsteroid(randomUpDown, AsteroidPrefabs[5]);
				asteroidWaitTimeCounter = 0;
			}
		}
	}
}

