using UnityEngine;

/**
 * Handle general player input (move paddle up & down).
*/
public class UserInput : MonoBehaviour
{
    public float moveSpeed;
    public string playerAxis;

    private Rigidbody2D _playerBody;

    void Start()
    {
        _playerBody = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        float axis = Input.GetAxis(playerAxis);
        _playerBody.AddForce(new Vector3(0.0f, axis, 0.0f) * moveSpeed);
    }
}
