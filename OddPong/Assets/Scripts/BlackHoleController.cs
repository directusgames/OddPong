using UnityEngine;
using System.Collections;

public class BlackHoleController : MonoBehaviour {
	
	public float rotationSpeed;
	public float scaleSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate(new Vector3(0,0,-rotationSpeed*Time.deltaTime));
		
		if(transform.localScale.x <= 1)
		{
			transform.localScale = new Vector3(transform.localScale.x + (scaleSpeed*Time.deltaTime), transform.localScale.y + (scaleSpeed*Time.deltaTime),1);
		}
	}			

	
	public IEnumerator ScaleDown()
	{
		while(transform.localScale.x >=	0)
		{
			transform.localScale = new Vector3(transform.localScale.x - scaleSpeed, transform.localScale.y - scaleSpeed,1);
		}
		
		Destroy (gameObject);	
		
		yield return new WaitForSeconds(0f);
	}
}
