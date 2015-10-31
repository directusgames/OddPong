using UnityEngine;

/**
 * Handle general player input (move paddle up & down).
*/
public class UserInput : MonoBehaviour
{
    public Rigidbody2D playerBody;
    public float moveSpeed;
    public KeyCode moveUp;
    public KeyCode moveDown;

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        float upDown = 0.0f;

        if (Input.GetKey(moveUp))
        {
            upDown = 1.0f;
        }
        else if (Input.GetKey(moveDown))
        {
            upDown = -1.0f;
        }

        playerBody.AddForce(new Vector3(0.0f, upDown, 0.0f) * moveSpeed);
    }
}
