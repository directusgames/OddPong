using UnityEngine;
using System.Collections;

public class GravityEffectController : MonoBehaviour {

	public int fadeSpeed;
	public int alphaVal;
	
	private int currentAlpha;


	// Use this for initialization
	void Start () {
	
		currentAlpha = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void FadeIn()
	{
		
		if(currentAlpha <= alphaVal)
		{
			currentAlpha = currentAlpha + fadeSpeed;
			newAlpha.a = currentAlpha;
			GetComponent<Renderer>().material.color.a = newAlpha;
		}
	}
	
	void FadeOut()
	{
	}
}
