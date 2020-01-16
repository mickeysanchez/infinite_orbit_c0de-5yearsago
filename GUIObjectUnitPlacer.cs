using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class GUIObjectUnitPlacer : MonoBehaviour {
	[System.Serializable]
	
	public class PixelPadding
	{
		public float LeftPadding;
		public float RightPadding;
		public float TopPadding;
		public float BottomPadding;
	}
	
	public PixelPadding Padding;
	
	public enum HALIGN {left=0, right=1};
	
	public enum VALIGN {top=0, bottom=1};
	
	public HALIGN HorzAlign = HALIGN.left;
	public VALIGN VertAlign = VALIGN.top;
	
	public GUICam GUICamera = null;
	
	private Transform ThisTransform = null;
	
	// Use this for initialization
	void Start () {
		ThisTransform = transform;
	}
	
	void OnGUI() {
		GUI.depth = 1;
	}
	
	// Update is called once per frame
	void Update () {
		// Calculate position on screen
		Vector3 FinalPosition = new Vector3 (0, 0, 0);
		// Offset with padding

		if (Padding.RightPadding > 0) 
		{
			FinalPosition = new Vector3 (FinalPosition.x + (Screen.width - Padding.RightPadding),
										 FinalPosition.y - (Padding.TopPadding) + (Padding.BottomPadding),
										 FinalPosition.z);

		}
		else
		{
			FinalPosition = new Vector3 (FinalPosition.x + (Padding.LeftPadding), 
			                             FinalPosition.y - (Padding.TopPadding),
			                             FinalPosition.z);

		}

		
		FinalPosition = new Vector3 (FinalPosition.x / GUICamera.PixelToWorldScale,
		                             FinalPosition.y / GUICamera.PixelToWorldScale,
		                             FinalPosition.z);

		FinalPosition = new Vector3 (FinalPosition.x - (float)this.renderer.bounds.size.x/2,
		                             FinalPosition.y + (float)this.renderer.bounds.size.y/2,
		                             FinalPosition.z);


		// Update position
		ThisTransform.localPosition = FinalPosition;
	}
}