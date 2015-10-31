using UnityEngine;
using System.Collections;

/**
 * Handle player 1 and player 2 input within the same script.
 * Assumes the 2d camera has a 'size' of 200.
 * 
 * If there is a controller plugged in, it assumes this is for player 2.
 * Unless we get time for UI/controller->player allocation.
*/
public class input : MonoBehaviour {
    public Transform playerTransform;
    public Rigidbody2D playerBody;
    public float moveSpeed = 0.5f;
    public bool playerOne;

	void Start () {
	
	}

    // Update is called once per frame
    void Update() {
        Vector3 curPos = playerTransform.position;

        // Player 1.
        if (playerOne) {
            if (Input.GetKey(KeyCode.W)) {
                playerBody.MovePosition(new Vector2(curPos.x, curPos.y + moveSpeed * Time.fixedDeltaTime));
            } else if (Input.GetKey(KeyCode.S)) {
                playerBody.MovePosition(new Vector2(curPos.x, curPos.y - moveSpeed * Time.fixedDeltaTime));
            }
        } else { // Player 2.
            if (Input.GetKey(KeyCode.I)) {
                playerBody.MovePosition(new Vector2(curPos.x, curPos.y + moveSpeed * Time.fixedDeltaTime));
            } else if (Input.GetKey(KeyCode.K)) {
                playerBody.MovePosition(new Vector2(curPos.x, curPos.y - moveSpeed * Time.fixedDeltaTime));
            }
        }
    }
}
