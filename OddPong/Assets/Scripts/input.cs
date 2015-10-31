using UnityEngine;
using System.Collections;

/**
 * Handle player 1 and player 2 input within the same script.
 * Assumes the 2d camera has a 'size' of 200.
 * 
 * If there is a controller plugged in, it assumes this is for player 2.
 * Unless we get time for UI/controller->player allocation.
*/
public class input : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public float moveSpeed = 0.5f;
    public bool arePlayerOne;

    void Start()
    {

    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        float upDown = 0.0f;

        // Player 1.
        if (arePlayerOne)
        {
            if (Input.GetKey(KeyCode.W))
            {
                upDown = 1.0f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                upDown = -1.0f;
            }
        }
        else
        { // Player 2.
            if (Input.GetKey(KeyCode.I))
            {
                upDown = 1.0f;
            }
            else if (Input.GetKey(KeyCode.K))
            {
                upDown = -1.0f;
            }
        }
        playerBody.AddForce(new Vector3(0.0f, upDown, 0.0f) * moveSpeed);
    }
}
