using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class GUIObject : MonoBehaviour {
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
		Vector3 FinalPosition = new Vector3 (HorzAlign == HALIGN.left ? 0.0f : Screen.width,
		                                    VertAlign == VALIGN.top ? 0.0f : -Screen.height,
		                                     ThisTransform.localPosition.z);

		// Offset with padding
		FinalPosition = new Vector3 (FinalPosition.x + (Padding.LeftPadding * Screen.width) - (Padding.RightPadding * Screen.width), 
		                            FinalPosition.y - (Padding.TopPadding * Screen.height) + (Padding.BottomPadding * Screen.height),
		                            FinalPosition.z);

		// Convert to pixel scale
		FinalPosition = new Vector3 (FinalPosition.x / GUICamera.PixelToWorldScale,
		                            FinalPosition.y / GUICamera.PixelToWorldScale,
		                            FinalPosition.z);

		// Update position
		ThisTransform.localPosition = FinalPosition;
	}
}
