using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public Animator animator;
	public GameObject ExplosionPrefab;
	public GameObject EnergyAcquireEffectPrefab;

	public GameObject PowerShield;
	public GameObject SpaceTimeBoostLabel;

	public GameObject AnimatedSpaceman;

	public float upSpeedLimit = 55.0f;
	public float upSpeed = 65f;
	public float downSpeed = 66f;
	public float TerminalVelocity = -18f; //maximum downwards velocity on any object. this exists in real life too! it also makes the game easier the closer to 0 you get (on the negative side).
	public float forwardMovementLimit = 0.12f;
	
	private float forwardMovementVel = 0;

	private float velocity = 0.0f;
	private bool movementAllowed = true;
	private bool beforeFirstPress = true;
	private bool gamePlayStarted = true;

	private float startingY;

	private bool hasFuel = true;

	private int powerUps = 0;
	private bool poweredUp = false;
	private float powerUpTimer = 0;
	public float powerUpTime = 9f;

	public float PoweredUpDownSpeedAddition = 33f;
	private float savedDownSpeed;

	private float TimeSincePlayerUpReleased;
	public float GravityDelayAfterUpReleased = 0.1f;//0.15f;
	
	private bool playerDownPosted = false;
	private bool velDownPosted = false;

	private bool playerUpPosted = false;
	private bool velUpPosted = false;

	private float poweredMovementDivider = 1;
	public float powerUpAccel = 0.1f;
	public float powerUpDecel = 0.018f;
	private bool powerUpEnding = false;
	private bool keyPressedLastFrame = false;
	private bool isFullFalling = false;
	public float FullFallVelocityThreshold = -10;
	
	public float StartingShieldBlinkInterval = 0.5f;
	private float blinkInterval = 0;
	private bool shieldBlinkToggler = false;
	private float shieldBlinkQuickener = .001f;
	private float shieldBlinkAdder = 0f;
	private float powerUpForwardMovementAdder = 1;
	
	public float MaxPoweredMovementDivider = 1.5f;

	private float timePowerUpEndingBegan;
	private bool powerUpEndingPosted;
	public float downSpeedFader = 0.01f;

	public GameObject PlayerShield;
	
	private Collider thisCollider;
	
	//Happens Before Any Start Function;
	void Awake () 
	{
		startingY = transform.position.y;
		savedDownSpeed = downSpeed;
		thisCollider = GetComponentInChildren<Collider>();
	}
	
	void Start() 
	{
		GameManager.Notifications.AddListener (this, "OnPlayerDeath");
		GameManager.Notifications.AddListener (this, "OnGameplayStart");
		GameManager.Notifications.AddListener (this, "PlayerReset");
		GameManager.Notifications.AddListener (this, "OutOfFuel");

		PowerShield.renderer.enabled = false;
		SpaceTimeBoostLabel.renderer.enabled = false;
		PlayerShield.renderer.enabled = false;

		if ( PlayerPrefs.HasKey("powerUps") ) {
			powerUps = PlayerPrefs.GetInt("powerUps");
		}
	}
	
	void OnDrawGizmos()
	{
//		Gizmos.color = Color.red;
//		Gizmos.DrawCube(colliderEndPosition, Vector3.one *0.5f);
		
		if (asteroidJumping)
		{
			Gizmos.color = new Color(0, 1, 1, 0.6f);
			Gizmos.DrawSphere(transform.position, 1);
		}
		
		if (boostingDown)
		{
			Gizmos.color = new Color(0.8f, 0.5f, 0.1f, 0.6f);
			Gizmos.DrawSphere(transform.position + Vector3.down, 1);
		}
		
		Gizmos.color = Color.green;
		Gizmos.DrawLine(transform.position, transform.position + (rayCastDir * characterHeight/2));	
	}
	
	private float characterHeight = 4f;
	
	void UpdateCollider()
	{
		colliderEndPosition = thisCollider.bounds.center - thisCollider.bounds.extents;
	}
	
	private float AsteroidJumpThreshold = 0.25f;
	private Vector3 colliderEndPosition = Vector3.zero;
	private bool asteroidJumping = false;
	private float asteroidJumpEffectStartTime = 999999;
	public float AsteroidJumpEffectDuration = 0.25f;
	private float JumpDistance = 1.5f;
	private float JumpHeight = 90f;
	
	void OnTriggerEnter (Collider other) 
	{
		if (other.CompareTag("Asteroid")) 
		{
			
			if ( (Time.time <= timePressedDown + AsteroidJumpThreshold) && (colliderEndPosition.y >= other.transform.position.y))
			{
				JumpAsteroid();
			}
			else
			{
				HitAsteroid (other);
			}
		} 

		else if (other.CompareTag("Energy"))
		{
			GotFuel (other);
		}

		else if (other.CompareTag ("PowerUp") )
		{
			PowerUp (other);
		}

		else if (other.CompareTag ("Shield") )
		{
			EngageShield(other);
		}
	}
	
	private void JumpAsteroid()
	{
		Debug.Log ("JumpAsteroid!");
		asteroidJumping = true;
		asteroidJumpEffectStartTime = Time.time;
		
		
		StartCoroutine(Jumping());
	}
	
	private float ParabolicJump(float x, float h, float d)
	{
		return -Mathf.Pow((((2*Mathf.Sqrt(h))/d) * x) - Mathf.Sqrt(h),2) + h;
	}

	private IEnumerator Jumping()
	{
		while(Time.time < asteroidJumpEffectStartTime + AsteroidJumpEffectDuration)
		{
			velocity = ParabolicJump(Time.time - asteroidJumpEffectStartTime , JumpHeight, JumpDistance);
			
			if (velocity > upSpeedLimit)
				velocity = upSpeedLimit;
				
			yield return null;
		}
		
//		velocity = 0;
		asteroidJumping = false;
		yield break;
	}

	private bool shielded = false;
	public float ShieldTimer = 6f;
	private float shieldTicker = 0;
	
	void EngageShield (Collider shieldPowerUp) 
	{	
		Destroy (shieldPowerUp.gameObject);
		shielded = true;
		PlayerShield.renderer.enabled = true;
	}

	void DisengageShield () 
	{
		shielded = false;
		PlayerShield.renderer.enabled = false;
		shieldTicker = 0;
	}

	void HitAsteroid (Collider asteroid) {
		#if !UNITY_WEBPLAYER
			Handheld.Vibrate ();
		#endif

		Destroy (asteroid.gameObject);
		Instantiate(ExplosionPrefab, asteroid.transform.position, Quaternion.identity);
		if (!shielded)
		{
			PreDeath ();
		}
		else 
		{
			DisengageShield();
		}
	}

	void PreDeath () {
		if (poweredUp) 
		{
			PowerDown ();
		}
		GameManager.Notifications.PostNotification(this, "PreDeath");
		AnimatedSpaceman.SetActive (false);
		StartCoroutine(PostDeathAfterSeconds(0.75f));
	}

	void GotFuel (Collider fuel) {
		hasFuel = true;
		Destroy (fuel.gameObject);
		GameManager.Notifications.PostNotification ( this, "GotFuel");
		Instantiate(EnergyAcquireEffectPrefab, transform.position, Quaternion.identity);
	}

	void OutOfFuel () {
		hasFuel = false;
	}

	private IEnumerator PostDeathAfterSeconds(float seconds)
	{
		yield return new WaitForSeconds (seconds);
		GameManager.Notifications.PostNotification (this, "OnPlayerDeath");
	}
	
	void OnPlayerDeath () {
		PlayerReset ();
	}

	void PowerUp (Collider powerup) {
		Destroy (powerup.gameObject);
		poweredUp = true;
		animator.CrossFade("FullBoost", .3f);
//		savedDownSpeed = downSpeed;
		downSpeed += PoweredUpDownSpeedAddition;
		PowerShield.renderer.enabled = true;
		SpaceTimeBoostLabel.renderer.enabled = true;
		powerUpTimer = 0;
		GameManager.Notifications.PostNotification(this, "PowerUp");
	}

	void PowerDown () {
		shieldBlinkAdder = 0f;
		shieldBlinkQuickener = .001f;
		blinkInterval = 0;
		powerUpTimer = 0;
		shieldBlinkToggler = false;
		GameManager.Notifications.PostNotification(this, "PowerDown");
		poweredUp = false;
		PowerShield.renderer.enabled = false;
		SpaceTimeBoostLabel.renderer.enabled = false;
		powerUpEnding = false;
		powerUpEndingPosted = false;
	}

	void PlayerReset () {
		poweredMovementDivider = 1;
		downSpeed = savedDownSpeed;
		
		asteroidJumping = false;
		poweredUp = false;
		shielded = false;

		gamePlayStarted = false;
		movementAllowed = false;
		beforeFirstPress = true;
		playerUpPosted = false;
		velUpPosted = false;
		velDownPosted = false;
		hasFuel = true;
		transform.position = new Vector3 (0, startingY, 0);
		velocity = 0.0f;
		forwardMovementVel = 0f;
		AnimatedSpaceman.SetActive (true);
		animator.Play ("HoverAnimation");
	}

	void OnGameplayStart () {
		gamePlayStarted = true;
		movementAllowed = true;
	}
	
	bool boostingDown = false;
	float boostDownStart = 9999999;
	
	float backwardDuration = 0.1f;
	bool boostingBack = false;
	private IEnumerator BackwardMove()
	{
		
		while (Time.time <= boostDownStart + backwardDuration)
		{
			yield return null;
		}
		boostingBack = false;
		StartCoroutine(FadeTo(0, 0.1f, 0.05f));
	}
	
	float downwardDuration = 0.2f;
	
	private IEnumerator DownwardMove()
	{
		
		while (Time.time <= boostDownStart + downwardDuration)
		{
			yield return null;
		}
		
		boostingDown = false;
		
		yield return null;
		//that means there's a very short time where the person can continually boost down
		if (!boostingDown)
		{
			animator.CrossFade ("FullFallToFullBoost", 0.2f);
		}
		
		yield break;
	}
	
	private IEnumerator FadeTo (float to, float threshold, float fadeTime)
	{
		float startTime = Time.time;
		while((forwardMovementVel - threshold > to || forwardMovementVel + threshold < to))
		{
			Debug.Log("inside FadeTo");
			forwardMovementVel = Mathf.Lerp(forwardMovementVel, to, (Time.time - startTime)/fadeTime);
			yield return null;
		}
		
		yield break;
	}


	void playerUp (float speed)
	{
		playerDownPosted = false;

		if (hasFuel) 
		{
			if (!playerUpPosted) 
			{
				playerUpPosted = true;
				GameManager.Notifications.PostNotification (this, "PlayerUp");
				//check if in soft fall or full fall
				AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
//				AnimatorStateInfo nextAnimatorStateInfo = animator.GetNextAnimatorStateInfo(0);

				if ( animatorStateInfo.IsName("FullFall") || animatorStateInfo.IsName("FloatToFullFall") )//&& velocity <= TerminalVelocity)
				{
					animator.CrossFade ("FullFallToFullBoost", 0.2f);

//					boostDownStart = Time.time;
//					boostingDown = true;
//					boostingBack = true;
//					StartCoroutine( BackwardMove() );
//					StartCoroutine( DownwardMove() );
				}
				else
				{
					animator.CrossFade ("FullBoost", 0.3f);
				}

			}
			
//			if (boostingDown)
//			{
//				if (boostingBack)
//				{
//					forwardMovementVel = Mathf.Lerp(forwardMovementVel, -55.49f, (Time.time-boostDownStart)/backwardDuration);
//					velocity -= speed * Mathf.Lerp(2, 0, (Time.time-boostDownStart)/backwardDuration) * Time.deltaTime;
//				}
//				else
//				{
//					velocity += speed * 2f * Time.deltaTime;
//				}
//				Debug.Log("BoostingDown");
//			}
			
			velocity += speed * Time.deltaTime;
			

			if (velocity > upSpeedLimit) 
			{
				velocity = upSpeedLimit;
			}

			if (velocity > 0) 
			{
				if (!velUpPosted) 
				{
					velUpPosted = true;
				}
			} 
			else 
			{
				velUpPosted = false;
			}

		} 
		else 
		{
			playerDown(downSpeed);
		}
	}


	void playerDown (float speed)
	{
		playerUpPosted = false;

		if (!playerDownPosted) {
			playerDownPosted = true;

			if (!poweredUp) {
				animator.CrossFade("BoostToFloat", 0.5f);
			}

			GameManager.Notifications.PostNotification (this, "PlayerDown");
		}

		if (velocity < 0) {
			if (!velDownPosted) 
			{
				velDownPosted = true;
			}
		} else {
			velDownPosted = false;
		}	

		velocity -= Mathf.Min((Mathf.Exp((Time.time - TimeSincePlayerUpReleased)/GravityDelayAfterUpReleased - 2)), 1.0f) * speed * Time.deltaTime; 

		if (velocity < TerminalVelocity) 
		{
			velocity = TerminalVelocity;
		}
	}

	void OnBecameInvisible () {
		GameManager.Notifications.PostNotification (this, "InitiateWarning");
	}

	void OnBecameVisible () {
		GameManager.Notifications.PostNotification (this, "EndWarning");
	}



	void enactPowerUp () {
		powerUpTimer += Time.deltaTime;

		if (powerUpTimer > powerUpTime) 
		{
			PowerDown ();
		}
		else
		{
			if (powerUpTimer >= powerUpTime - 4) {
				enactPowerUpEnding ();
			} else 
			{

				if (poweredMovementDivider < MaxPoweredMovementDivider) 
				{
					poweredMovementDivider += powerUpAccel;
				}
			}
		}
	}


	void enactPowerUpEnding () {
		if (!powerUpEndingPosted)
		{
			timePowerUpEndingBegan = Time.time;
			powerUpEndingPosted = true;
		}

		blinkInterval += (Time.deltaTime + shieldBlinkAdder);
		shieldBlinkAdder += shieldBlinkQuickener;
		
		if (blinkInterval > StartingShieldBlinkInterval) 
		{
			shieldBlinkToggler = shieldBlinkToggler ? false : true;
			blinkInterval = 0;
		}
		
		if (shieldBlinkToggler)
		{
			PowerShield.renderer.enabled = false;
		} 
		else
		{
			PowerShield.renderer.enabled = true;
		}

		if (downSpeed > savedDownSpeed)
		{
			downSpeed = Mathf.Lerp (downSpeed, savedDownSpeed, (Time.time - timePowerUpEndingBegan)/4 );
		}

		if (poweredMovementDivider > 1) {
			poweredMovementDivider -= powerUpDecel * Time.deltaTime;
		}

	}
	
	private float timePressedDown = 0;
	
	private Vector3 rayCastDir = Vector3.forward - Vector3.up;
	
	private void OnButtonDown()
	{
//		Debug.Log(Time.time + " : ButtonDown");
		timePressedDown = Time.time;
		isFullFalling = false;
		keyPressedLastFrame = true;
		
		
		//do a raycast
		//if asteroid beneath, 
		//then start jumping coroutine
//		RaycastHit hit;
//		
//		if (Physics.Raycast(transform.position, rayCastDir, out hit, characterHeight/2))
//		{
//			JumpAsteroid();
//		}
									
	}
	
	private void OnButtonUp()
	{
		TimeSincePlayerUpReleased = Time.time;
		keyPressedLastFrame = false;
	}

	void Update () 
	{
		UpdateCollider();
		
		if (AnimatedSpaceman.activeSelf) 
		{
			animator.SetFloat ("PlayerVelocity", velocity); //set from here so that we can use it for state transition conditionals in the Animator graph
		}

		if (movementAllowed) 
		{
			if (Input.anyKey) 
			{
					powerUpForwardMovementAdder = (poweredUp && !powerUpEnding) ? 1.2f : 0;
					if (transform.position.z < forwardMovementLimit + powerUpForwardMovementAdder) 
					{ 
						forwardMovementVel = 3;
					}
	
					if (beforeFirstPress) 
					{
						beforeFirstPress = false;
						GameManager.Notifications.PostNotification(this, "AfterFirstPress");
					}
	
					//on ButtonDown
					if (!keyPressedLastFrame)
					{
						Debug.Log("OnButtonDown");
						OnButtonDown();	
					}
	
				playerUp(upSpeed);
				
			}
			else 
			{
				if (!beforeFirstPress) 
				{
					if (velocity <= FullFallVelocityThreshold && !isFullFalling) //its important that this if statement happen before playerDown
					{
						if (!poweredUp) {
							animator.CrossFade("FloatToFullFall", 0.6f);
							isFullFalling = true;
						}
					}

					if (transform.position.z > -.75f + powerUpForwardMovementAdder) 
					{
						if (poweredUp) 
						{
							forwardMovementVel = -1.5f;
						}
						else
						{
							forwardMovementVel = -3;
						}
					}
					else
					{
						forwardMovementVel = 0;
					}

					//this is a button release
					if (keyPressedLastFrame)
					{
						Debug.Log("On Key Released");
						OnButtonUp ();
					}

					playerDown (downSpeed);
				}
			}
		}

		if (poweredUp) 
		{
			enactPowerUp ();
		} 
		else
		{
			poweredMovementDivider -= powerUpDecel;
			if (poweredMovementDivider > 1) {
				poweredMovementDivider -= powerUpDecel;
			} else {
				poweredMovementDivider = 1;
			}

			if (savedDownSpeed != null && downSpeed > savedDownSpeed) {
				downSpeed -= downSpeedFader * Time.deltaTime;
			}
		}

		if (shielded) 
		{
			shieldTicker += Time.deltaTime;

//			Debug.Log (ShieldTimer);

			if (shieldTicker > ShieldTimer)
			{
				DisengageShield();
			}
		}

		transform.position = transform.position + new Vector3 (0, velocity/poweredMovementDivider * Time.deltaTime, forwardMovementVel * Time.deltaTime);
	}
}