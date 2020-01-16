using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GUIController : MonoBehaviour {
	public GUIStyle myStyle;
	public GUIStyle myStyle2;
	public GUIStyle style3;

	public float LastTimeOffset = 100f;
	public float BestTimeOffset = 120f;

	private float startTime = 0.0f;
	private string textTime;

	public GameObject PlayButton;
	public GameObject MainMenu;
	public GameObject PauseButton;

	public GameObject[] Instructions;

	public GameObject HoldDownToGoUpButton;
	public GameObject CollectEnergyArrows;
	public GameObject CollectEnergyText;
	public GameObject FlightInfoBackground;
	public GameObject ShareButton;
	private SpriteRenderer FlightInfoBackgroundRenderer;
	public GameObject FlightTimeLabel;
	private SpriteRenderer FlightTimeLabelRenderer;
	public GameObject FuelLabel;
	private SpriteRenderer FuelLabelRenderer;

	private float bestScore = 0.0f;
	private float lastScore = 0.0f;
	private string bestDisplayTime = "00:00:00";
	private string lastDisplayTime = "00:00:00";

	private bool gameplayStarted = true;
	private bool afterFirstPress = false;

	public float MaxFuel = 50f;
	private float fuel;
	
	
//	private class EnergyBlock
//	{
//		private float capacity;
//		private float currentAmount;
//		public float CurrentAmount { get { return currentAmount; } }
//		
//		public EnergyBlock(float capacity)
//		{
//			currentAmount = capacity;
//		}
//		
//		public bool Regen(float regenRate)
//		{
//			currentAmount += regenRate * Time.deltaTime;
//			
//			if (currentAmount >= capacity)
//			{
//				currentAmount = capacity;
//			}
//			
//			return true;
//		}
//		
//		public float Degen(float degenRate)
//		{
//			currentAmount -= degenRate * Time.deltaTime;
//			
//			if (currentAmount > 0)
//				return 0;
//			else
//				return currentAmount; //the remainder of the energy that needs to be expended on the next block
//		}
//
//	}
//	
//	private class EnergyBar
//	{
//		private Rect rect;
//		private int maxBlocks;
//		private float blockCapacity;
//		
//		LinkedList<EnergyBlock> blocks;
//		LinkedListNode<EnergyBlock> itr;
//		
//		public EnergyBar(Rect rect, float blockCapacity, int maxBlocks)
//		{
//			this.rect = rect;
//			this.maxBlocks = maxBlocks;
//			this.blockCapacity = blockCapacity;
//			
//			blocks = new LinkedList<EnergyBlock>();
//			
//			for (int i = 0; i < maxBlocks; i++)
//			{
//				AddEnergyBar();
//			}
//							
//			itr = blocks.Last;
//		}
//		
//		public void AddEnergyBar()
//		{
//			blocks.AddFirst(new EnergyBlock(blockCapacity));
//		}
//		
//		public bool Expend(float SomeEnergyPerSecond)
//		{
//			//if returns true then able to do the skill, otherwise, cannot
//			
//			
////			expends the energy from the current bar
//				return true;
//		}
//		
//		void Draw() //called from OnGUI()
//		{
//			//for now just draw each block as a 50 x 10 pixel bar, with a 5 pixel border
//			foreach (EnergyBlock block in blocks)
//			{
////				GUI.Box(new Rect(
//			}
//		}
//	}
	
	public Sprite sprite;
	private Texture fuelOutline;
	public Sprite sprite2;
	private Texture redFuelOutline;
	public Sprite sprite3;
	private Texture fuelBar;
	public float FuelRegenSpeed = 1f;

	private float fuelBarWidth;

//	private BoxCollider[] Colliders = null;

	void Start() {
		GameManager.Notifications.AddListener (this, "OnPlayerDeath");
		GameManager.Notifications.AddListener (this, "PreDeath");
		GameManager.Notifications.AddListener (this, "OnGameplayStart");
		GameManager.Notifications.AddListener (this, "AfterFirstPress");
		GameManager.Notifications.AddListener (this, "GamePaused");
		GameManager.Notifications.AddListener (this, "GameUnpaused");
		GameManager.Notifications.AddListener (this, "PlayerUp");
		GameManager.Notifications.AddListener (this, "PlayerDown");
		GameManager.Notifications.AddListener (this, "PowerUp");
		GameManager.Notifications.AddListener (this, "PowerDown");
		GameManager.Notifications.AddListener (this, "GotFuel");

		FlightInfoBackgroundRenderer = FlightInfoBackground.GetComponent<SpriteRenderer> ();
		FlightInfoBackgroundRenderer.enabled = false;

		FlightTimeLabelRenderer = FlightTimeLabel.GetComponent<SpriteRenderer> ();
		FuelLabelRenderer = FuelLabel.GetComponent<SpriteRenderer> ();

		ShareButton.SetActive (false);
		PlayButton.SetActive (false);
		MainMenu.SetActive (false);
		PauseButton.SetActive (true);

		for (int i = 0; i < Instructions.Length; i++) {
			Instructions[i].SetActive(true);
		}

//		Colliders = GetComponentsInChildren<BoxCollider> ();

		if (PlayerPrefs.HasKey ("bestScore")) {
			bestScore = PlayerPrefs.GetFloat("bestScore");

			int minutes2 = (int)bestScore / 60;
			int seconds2 = (int)bestScore % 60;
			int fraction2 = (int)(bestScore * 100) % 100;
			bestDisplayTime = string.Format("{0:00}:{1:00}:{2:00}", minutes2, seconds2, fraction2);
		}

		var croppedTexture = new Texture2D( (int)sprite.rect.width, (int)sprite.rect.height );
		
		var pixels = sprite.texture.GetPixels(  (int)sprite.textureRect.x, 
		                                      (int)sprite.textureRect.y, 
		                                      (int)sprite.textureRect.width, 
		                                      (int)sprite.textureRect.height );
		
		croppedTexture.SetPixels( pixels );
		croppedTexture.Apply();

		fuelOutline = croppedTexture;

		var croppedTexture3 = new Texture2D( (int)sprite3.rect.width, (int)sprite3.rect.height );
		
		var pixels3 = sprite3.texture.GetPixels(  (int)sprite3.textureRect.x, 
		                                      (int)sprite3.textureRect.y, 
		                                      (int)sprite3.textureRect.width, 
		                                      (int)sprite3.textureRect.height );
		
		croppedTexture3.SetPixels( pixels3 );
		croppedTexture3.Apply();
		
		redFuelOutline = croppedTexture3;


		var croppedTexture2 = new Texture2D( (int)sprite2.rect.width, (int)sprite2.rect.height );
		
		var pixels2 = sprite2.texture.GetPixels(  (int)sprite2.textureRect.x, 
		                                      (int)sprite2.textureRect.y, 
		                                      (int)sprite2.textureRect.width, 
		                                      (int)sprite2.textureRect.height );
		
		croppedTexture2.SetPixels( pixels2 );
		croppedTexture2.Apply();
		
		fuelBar = croppedTexture2;

		fuelBarWidth = Screen.width/2f;
		
		fuel = MaxFuel;
	}

	void GamePaused () {
		MainMenu.SetActive (true);
		PlayButton.SetActive (true);
	}

	void GameUnpaused () {
		MainMenu.SetActive (false);
		PlayButton.SetActive (false);
	}

	void OnGameplayStart () {
		poweredUp = false;
		FlightTimeLabelRenderer.enabled = true;
		FuelLabelRenderer.enabled = true;
		FlightInfoBackgroundRenderer.enabled = false;
		PauseButton.SetActive (true);

		for (int i = 0; i < Instructions.Length; i++) {
			Instructions[i].SetActive(true);
		}

		afterFirstPress = false;
		gameplayStarted = true;
		HideOptions ();
	}

	void AfterFirstPress () {

		for (int i = 0; i < Instructions.Length; i++) {
			Instructions[i].SetActive(false);
		}

		afterFirstPress = true;
		StartTimer ();
	}

	void PreDeath()
	{
		StopTimer ();
	}

	void OnPlayerDeath () {
		GameManager.LastScore = lastScore;

		if (lastScore > bestScore) {
			PlayerPrefs.SetFloat("bestScore", lastScore);
			PlayerPrefs.Save ();
			bestScore = lastScore;

			int minutes2 = (int)bestScore / 60;
			int seconds2 = (int)bestScore % 60;
			int fraction2 = (int)(bestScore * 100) % 100;
			bestDisplayTime = string.Format("{0:00}:{1:00}:{2:00}", minutes2, seconds2, fraction2);
		}

		int minutes = (int)lastScore / 60;
		int seconds = (int)lastScore % 60;
		int fraction = (int)(lastScore * 100) % 100;
		lastDisplayTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);

		FlightInfoBackgroundRenderer.enabled = true;
		FlightTimeLabelRenderer.enabled = false;
		FuelLabelRenderer.enabled = false;
		PauseButton.SetActive (false);
		gameplayStarted = false;
		afterFirstPress = false;
		fuel = MaxFuel;
		hasFuel = true;
		ShowOptions ();

		currentTick = tickInterval;
		currentFlightTime = 0;
	}

	private bool countTime = false;

	public void StartTimer() {
		countTime = true;
		startTime = Time.time;
		currentTick = tickInterval;
	}


	public void StopTimer() {
		countTime = false;
//		lastScore = Time.time - startTime;
		lastScore = currentFlightTime;
		startTime = 0.0f;
	}

	private void ShowOptions() 
	{
		MainMenu.SetActive (true);
		PlayButton.SetActive (true);
		ShareButton.SetActive (true);
//		PauseButton.SetActive (false);
	}

	private void HideOptions() 
	{
		MainMenu.SetActive (false);
		PlayButton.SetActive (false);
		ShareButton.SetActive (false);
//		PauseButton.SetActive (true);
	}

	public float fuelAdder = 12.5f;
	void GotFuel () {
		fuel += fuelAdder;
		fuel = fuel > MaxFuel ? MaxFuel : fuel;
		hasFuel = true;
	}

	private bool jetsOn = false;
	private bool hasFuel = true;

	public float FuelLossSpeed = 50;

	void PlayerUp () {
		jetsOn = true;
	}

	void PlayerDown () {
		jetsOn = false;
	}


	void Update () 
	{

		// currentFlightTime = Time.time - startTime; //this will give incorrect flight times if you pause :(
		// Moved this to Update because OnGUI is called multiple times per frame.
		if (countTime)
		{
			if (poweredUp) 
			{
				currentFlightTime += Time.deltaTime * 2;
			}
			else
			{
				currentFlightTime += Time.deltaTime;
			}
			
			currentTick -= Time.deltaTime;
			
			if (currentTick <= 0) 
			{
				Tick (	);
			}
		}


		if (jetsOn && afterFirstPress && fuel > 0) 
		{
			fuel -= FuelLossSpeed * Time.deltaTime;
		} 
		else if (!jetsOn && fuel < MaxFuel) 
		{
			fuel += FuelRegenSpeed * Time.deltaTime * 0.01f;		
		}

		if (fuel <= 0) {
			fuel = 0;
			if (hasFuel) {
				GameManager.Notifications.PostNotification (this, "OutOfFuel");
				hasFuel = false;
			}
		} else if (fuel > MaxFuel) {
			fuel = MaxFuel;
		}
	}

	private void Tick()
	{
		currentTick = tickInterval;
		tickString = textTime;
		StartCoroutine (DisplayTickInterval (0.75f));
	}

	bool showingTick = false;
	float verticalPos;
	string tickString;

	private IEnumerator DisplayTickInterval(float forSeconds)
	{
		showingTick = true;

		float started = currentFlightTime;
		verticalPos = Screen.height/19;

		while (currentFlightTime < started + forSeconds) 
		{
			verticalPos += Screen.height/200;
			yield return null;
		}

		showingTick = false;
		yield break;
	}
	
	private float currentTick = 5;
	private float tickInterval = 5;
	private bool poweredUp = false;
	void PowerUp()
	{
		poweredUp = true;
		tickInterval = 2.5f;
	}

	void PowerDown()
	{
		poweredUp = false;
		tickInterval = 5;
	}

// Like Update, but allows you to call GUI functions.
	float currentFlightTime = 0;
	private bool warningFuelToggler = false;
	private float warningFuelCounter = 0;
	private float warningFuelBlinkSpeed = .4f;
	void OnGUI () {
		if (afterFirstPress) {

			if (showingTick)
			{
				GUI.Box (new Rect (Screen.width/2 - 100, verticalPos, 200, 25), tickString, myStyle);
			}

			int minutes = (int)currentFlightTime / 60;
			int seconds = (int)currentFlightTime % 60;
			int fraction = (int)(currentFlightTime * 100) % 100;

			textTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, fraction);
			GUI.Box ( new Rect (Screen.width/2 - 100, Screen.height/19, 200, 25), textTime, myStyle);

			if (fuel > MaxFuel * 0.333f)
			{
				GUI.DrawTexture( new Rect(Screen.width/2 - fuelBarWidth/2, Screen.height/10*1.25f, fuelBarWidth, 14.5f), fuelOutline, ScaleMode.StretchToFill, true, 10.0F);
			}
			else 
			{
				FuelWarning();
			}

			GUI.DrawTexture( new Rect(Screen.width/2 - fuelBarWidth/2, Screen.height/10*1.25f, fuelBarWidth * (fuel/MaxFuel), 14), fuelBar, ScaleMode.StretchToFill, true, 10.0F);
		}
		else {
			if (gameplayStarted) 
			{
				GUI.Box ( new Rect (Screen.width/2 - 100, Screen.height/19, 200, 25), "00:00:00", myStyle);

				GUI.DrawTexture( new Rect(Screen.width/2 - fuelBarWidth/2, Screen.height/10*1.25f, fuelBarWidth, 14.5f), fuelOutline, ScaleMode.StretchToFill, true, 10.0F);
				GUI.DrawTexture( new Rect(Screen.width/2 - fuelBarWidth/2, Screen.height/10*1.25f, fuelBarWidth * (fuel/MaxFuel), 14), fuelBar, ScaleMode.StretchToFill, true, 10.0F);
			}
			else
			{
				GUI.Box ( new Rect (Screen.width/2 - 98, Screen.height/BestTimeOffset + 2, 200, 25), bestDisplayTime, myStyle2);
				GUI.Box ( new Rect (Screen.width/2 - 98, Screen.height/LastTimeOffset + 2, 200, 25), lastDisplayTime, myStyle2);
				GUI.Box ( new Rect (Screen.width/2 - 100, Screen.height/BestTimeOffset, 200, 25), bestDisplayTime, myStyle);
				GUI.Box ( new Rect (Screen.width/2 - 100, Screen.height/LastTimeOffset, 200, 25), lastDisplayTime, myStyle);

			}
		}
	}

	void FuelWarning() {
		warningFuelCounter += Time.deltaTime;

		if (warningFuelCounter > warningFuelBlinkSpeed) {
			warningFuelCounter = 0;
			warningFuelToggler = warningFuelToggler ? false : true;
		}

		if (warningFuelToggler) {
			GUI.DrawTexture( new Rect(Screen.width/2 - fuelBarWidth/2, Screen.height/10*1.25f, fuelBarWidth, 14.5f), redFuelOutline, ScaleMode.StretchToFill, true, 10.0F);
		} else {
			GUI.DrawTexture( new Rect(Screen.width/2 - fuelBarWidth/2, Screen.height/10*1.25f, fuelBarWidth, 14.5f), fuelOutline, ScaleMode.StretchToFill, true, 10.0F);
		}
	}

	void OnLevelWasLoaded() {
		lastScore = 0;
		startTime = 0.0f;
		countTime = false;
	}
}