using UnityEngine;
using System.Collections;

public class MultiballerController : MonoBehaviour {
	
	public int numBalls, ballSpeed;
	
	public GameObject attractor;
	
	public BallManager ballManager;
	
	
	// Use this for initialization
	void Start () {
	
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Ball")
		{
			Debug.Log ("Ball entered inner collider");
			Destroy (coll.gameObject);
			
			SpawnMultiBalls();
			
		}		
	}
	
	void SpawnMultiBalls()
	{
		
		//Disable attractor
		attractor.SetActive(false);
		
		//Disable collider
		GetComponent<CircleCollider2D>().enabled = false;
		
		for(int x = 1; x <= numBalls; x++)
		{	
			//If number is odd, send ball left. If even, send right.
			if(x % 2 != 0)
			{				
				ballManager.SpawnBall(transform.position, new Vector3(-1, Random.Range (-1f,1f), 0) * ballSpeed);
			}
			
			else
			{
				ballManager.SpawnBall(transform.position, new Vector3(1, Random.Range (-1f,1f), 0) * ballSpeed);
			}
		}
		
		transform.parent.gameObject.GetComponent<BlackHoleController>().ScaleDown();
	}
}
