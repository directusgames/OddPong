using UnityEngine;
using System.Collections;

public class BallMovement : MonoBehaviour {
    public int startSpeed;

    public GameObject racquetLeft, racquetRight;
	
	//Check which section of racquet the ball hits
	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight) {
		
		//1 = top of paddle
		//2 = middle of paddle
		//3 = bottom of paddle
		return (ballPos.y - racketPos.y) / racketHeight;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		
		if (col.gameObject.name == racquetLeft.name) {
		
			float y = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
			
			Vector2 dir = new Vector2(1, y).normalized;
			
			GetComponent<Rigidbody2D>().velocity = dir * startSpeed;
		}
		
		if (col.gameObject.name == racquetRight.name) {

			float y = hitFactor(transform.position,
			                    col.transform.position,
			                    col.collider.bounds.size.y);
			
			Vector2 dir = new Vector2(-1, y).normalized;
			
			GetComponent<Rigidbody2D>().velocity = dir * startSpeed;
		}
	}
}
