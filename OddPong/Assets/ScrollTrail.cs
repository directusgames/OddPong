using UnityEngine;
using System.Collections;

public class ScrollTrail : MonoBehaviour {

	public float speed;
	public float start = 0;
	
	Vector2 offset = new Vector2 (0, 0);
	
	void Update ()
	{
		offset.x = start + Time.time * speed;
		
		GetComponent<TrailRenderer>().material.mainTextureOffset = offset;
	}
}
