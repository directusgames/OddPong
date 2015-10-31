using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	
	public int speed;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey (KeyCode.D))
		{
			Vector3 pos = transform.position;
			pos.x += speed;
			transform.position = pos;
		}
	}
}
