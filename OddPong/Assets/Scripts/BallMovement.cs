using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
	
	public int startSpeed;
	
	// Use this for initialization
	void Start () {
		
		float rotation = Random.Range (0,360);		
		Vector3 rotationVec = new Vector3(0,0,rotation);
		transform.rotation = Quaternion.Euler(rotationVec);
		gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 5000);
		
	}
	
	// Update is called once per frame
	void Update () {
		//send off in random direction at set velocity
	
		
		
	}
}
