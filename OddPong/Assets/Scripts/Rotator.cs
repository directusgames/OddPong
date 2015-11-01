using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public float RotateSpeed = 50.0f;

    void Update()
    {
        // Note, the body being transformed should have a "Rigidbody" component.
        //
        // If we didn't do this, Unity would be recalculating a lot of things
        // each frame rather than on demand (since it's otherwise expecting
        // this object to be static).
        //
        // Use "Is Kinematic" to prevent physics interactions.
        transform.Rotate(new Vector3(0.0f, 0.0f, -RotateSpeed) * Time.deltaTime);
    }
}
