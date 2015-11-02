using UnityEngine;
using System.Collections;

public class BallCollide : MonoBehaviour
{
    public GameManager m_mainScript;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
		if(coll.gameObject.tag == "Ball")
		{
	        m_mainScript.playerScores(this.gameObject.tag);
	        Debug.Log ("player scored");
	    }
    }
}
