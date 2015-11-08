using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallTextureEvent : MonoBehaviour, GameEvent {

	//public float fadeTime, fadeSpeed;
	public Material originalTrailMat, newTrailMat;
	public float runTime;
	
	private GameObject ball;
	
	private float timeRunning;
	private bool running;
	
	private GameObject alertMgr;

	// Use this for initialization
	void Start () {
		
		timeRunning = 0;
		running = false;
		
		alertMgr = GameObject.Find ("AlertManager");
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(running)
		{
			timeRunning += Time.deltaTime;
		
			if(timeRunning > runTime)
			{
				StopRandomEvent();
			}
		}
	}
	
	public void StartRandomEvent()
	{
		Invoke("ChangeTexture", 1.5f);
		alertMgr.GetComponent<AlertManager>().ShowAlert("INVISIBALL");
		timeRunning = 0;
			
		
	}
	
	public void StopRandomEvent()
	{
		Debug.Log ("Ball event stopped");
		if(ball != null)
		{
			ball.transform.GetChild(0).GetComponent<Renderer>().material = originalTrailMat;
			ball.transform.GetChild(1).gameObject.SetActive(false);
			running = false;
		}
		else
		{
			running = false;
		}	
	}
	
	void ChangeTexture()
	{
		running = true;
		Debug.Log ("Ball event started");
		
		ball = GameObject.FindGameObjectWithTag("Ball");
		
		ball.transform.GetChild(0).GetComponent<Renderer>().material = newTrailMat;
		ball.transform.GetChild (1).gameObject.SetActive(true);
	}
}
