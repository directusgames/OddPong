using UnityEngine;
using System.Collections;

public class ScrollBackground : MonoBehaviour {

	public float speed = 0.5f;
	public float start = 0;
	
	Vector2 offset = new Vector2 (0, 0);
	
	void Update ()
	{
		offset.y = start + Time.time * speed;
		
		GetComponent<MeshRenderer>().material.mainTextureOffset = offset;
	}
	
}
