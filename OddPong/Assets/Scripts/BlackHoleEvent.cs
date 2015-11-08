using UnityEngine;
using System.Collections;

public class BlackHoleEvent : MonoBehaviour, GameEvent {
	
	private float camHeight;
	private Vector3 spawnPos;
	private GameObject bh, alertMgr;
	
	public GameObject blackHole;
	// Use this for initialization
	void Start () {
		
		alertMgr = GameObject.Find ("AlertManager");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void StartRandomEvent()
	{
		camHeight = Camera.main.orthographicSize / 2.0f;
		spawnPos = new Vector3(0, Random.Range (camHeight, -camHeight), 0);
		
		Invoke ("SpawnBlackHole", 1.5f);
		alertMgr.GetComponent<AlertManager>().ShowAlert("BLACK HOLE");
		
	}
	
	public void StopRandomEvent()
	{	
		if(bh != null)
		{
			Destroy (bh);
		}
	}
	
	void SpawnBlackHole()
	{
		bh = (GameObject)Instantiate(blackHole, spawnPos, Quaternion.identity);
	}
}
