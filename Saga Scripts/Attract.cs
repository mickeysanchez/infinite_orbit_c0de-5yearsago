using UnityEngine;
using System.Collections;

public class Attract : MonoBehaviour 
{
	private GameObject To;
	private float Distance = 5f;
	private float distSqr;
	private float Speed = 10.0f;
	public float velocity = -55.94f;
	private float timeOnPlayer = 0;
	private bool hasTarget = false;
	
	// Use this for initialization
	
	void Start () 
	{
		distSqr = Mathf.Pow(Distance, 2);
	}
	
	void Awake()
	{
		To = GameObject.FindGameObjectWithTag("Player");
	}
	
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position, Distance);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!hasTarget)
		{
			Vector3 sub = transform.position - To.transform.position;
				
			if ((sub.sqrMagnitude <= distSqr))
			{
				hasTarget = true;
			}
		}
		else
		{
			transform.position = Vector3.Lerp (transform.position, To.transform.position, Time.deltaTime * Speed * (1+ timeOnPlayer));
			timeOnPlayer += 0.1f;
			velocity += timeOnPlayer;
		}
		
		transform.position += new Vector3(0,0, Mathf.Min(velocity, 0) * Time.deltaTime);
	}
}
