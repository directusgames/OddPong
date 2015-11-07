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

	// Use this for initialization
	void Start () {
		
		timeRunning = 0;
		running = false;
		
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
		running = true;
		Debug.Log ("Ball event started");
		
		ball = GameObject.FindGameObjectWithTag("Ball");
		
		ball.transform.GetChild(0).GetComponent<Renderer>().material = newTrailMat;
		ball.transform.GetChild (1).gameObject.SetActive(true);
		
//		Invoke ("EffectOn",5);
	
	
//		balls = GameObject.FindGameObjectsWithTag("Ball");
//		
//		foreach(GameObject ball in balls)
//		{
//			//Show sphere
//			//Show new trail renderer material
//			
//			//Hide ball
//			//Hide old trail renderer material
//		}	
		
	}
	
	public void StopRandomEvent()
	{
		ball.transform.GetChild(0).GetComponent<Renderer>().material = originalTrailMat;
		ball.transform.GetChild(1).gameObject.SetActive(false);
		running = false;
	}
	
//	public void EffectOn()
//	{
//		Debug.Log ("Effect change started");
//		InvokeRepeating("FadeInTrail",0,fadeTime);
//	}
//	
////	public void EffectOff()
////	{
////		InvokeRepeating("FadeOut",0,fadeTime);
////	}
//	
//	void FadeInTrail()
//	{
//		Debug.Log ("Fading in");
//		if(currentTrailMat.color.a < 255)
//		{
//			currentAlpha = currentAlpha + fadeSpeed;
//			newTrailMat.color = new Color(newTrailMat.color.r, newTrailMat.color.g, newTrailMat.color.b, currentAlpha/255);
//			currentTrailMat.color = new Color(currentTrailMat.color.r, currentTrailMat.color.g, currentTrailMat.color.b, (255-currentAlpha)/255);
//		}
//		
//		else
//		{
//			CancelInvoke ("FadeInTrail");
//			
//			//switch the materials
//			Material tempMat = currentTrailMat;
//			currentTrailMat = newTrailMat;
//			newTrailMat = tempMat;
//			
//			currentAlpha = 0;
//		}
//	}
	
//	void FadeInSphere()
//	{		
//		if(currentAlpha <= alphaVal)
//		{
//			currentAlpha = currentAlpha + fadeSpeed;
//			GetComponent<Renderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, currentAlpha/255);
//		}
//		else
//		{
//			CancelInvoke("FadeIn");
//		}
//	}
//	
//	void FadeOutSphere()
//	{
//		if(currentAlpha > 0)
//		{
//			currentAlpha = currentAlpha - fadeSpeed;
//			GetComponent<Renderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, currentAlpha/255);
//		}
//		else
//		{
//			CancelInvoke("FadeOut");
//		}
//	}
}
