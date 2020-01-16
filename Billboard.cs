using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour 
{
	public Vector3 Offset = Vector3.zero;

	private Vector3 eulerAngles;

	void Update () 
	{
		transform.rotation = Quaternion.LookRotation(Vector3.Normalize(transform.position - Camera.main.transform.position));
		transform.rotation = Quaternion.Euler (Offset + transform.eulerAngles);
	}
}
