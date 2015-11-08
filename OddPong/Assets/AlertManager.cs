using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AlertManager : MonoBehaviour {

	public Text txtAlert;
	public AudioSource alertSound;
	
	public float lifetime, repeatTime;
	
	private float currentTime;
	private bool alertShowing;
	
	// Use this for initialization
	void Start () {
		
		alertShowing = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if(alertShowing)
		{
			currentTime += Time.deltaTime;
			
			if(currentTime >= lifetime)
			{
				CancelInvoke("FlashText");
				txtAlert.enabled = false;
				currentTime = 0;
				alertShowing = false;
			}
		}	
	}
	
	public void ShowAlert(string alertText)
	{
		alertShowing = true;
		txtAlert.enabled = true;
		currentTime = 0;
		txtAlert.text = alertText;
		txtAlert.color = Color.white;
		alertSound.Play ();
		InvokeRepeating("FlashText",0,repeatTime);
	}
	
	void FlashText()
	{
		if(txtAlert.color == Color.white)
		{
			txtAlert.color = Color.red;
		}
		else
		{
			txtAlert.color = Color.white;
		}
	}
}
