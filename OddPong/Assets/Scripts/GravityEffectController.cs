using UnityEngine;
using System.Collections;

public class GravityEffectController : MonoBehaviour {

	public float fadeSpeed;
	public float fadeTime;
	public float alphaVal;
	
	private float currentAlpha;
	private Color baseColor;
	
	// Use this for initialization
	void Start () {
		
		baseColor = GetComponent<Renderer>().material.color;
		Debug.Log ("Effect color: " + GetComponent<Renderer>().material.color);
		currentAlpha = 0;
		//	EffectOn ();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void EffectOn()
	{
		InvokeRepeating("FadeIn",0,fadeTime);
	}
	
	public void EffectOff()
	{
		InvokeRepeating("FadeOut",0,fadeTime);
	}
	
	void FadeIn()
	{		
		if(currentAlpha <= alphaVal)
		{
			currentAlpha = currentAlpha + fadeSpeed;
			GetComponent<Renderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, currentAlpha/255);
		}
		else
		{
			CancelInvoke("FadeIn");
		}
	}
	
	void FadeOut()
	{
		if(currentAlpha > 0)
		{
			currentAlpha = currentAlpha - fadeSpeed;
			GetComponent<Renderer>().material.color = new Color(baseColor.r, baseColor.g, baseColor.b, currentAlpha/255);
		}
		else
		{
			CancelInvoke("FadeOut");
		}
	}
}
