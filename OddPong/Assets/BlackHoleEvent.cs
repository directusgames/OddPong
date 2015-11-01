using UnityEngine;
using System.Collections;

public class BlackHoleEvent : MonoBehaviour, GameEvent {

	public GameObject blackHole;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartRandomEvent()
	{
		float camHeight = Camera.main.orthographicSize / 2.0f;
		Vector3 spawnPos = new Vector3(0, Random.Range (camHeight, -camHeight), 0);
		
		Instantiate(blackHole, spawnPos, Quaternion.identity);
	}
	
	public void StopRandomEvent()
	{	
		
	}
}
