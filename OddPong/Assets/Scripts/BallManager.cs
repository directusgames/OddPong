using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject racquetLeft;
    public GameObject racquetRight;

    public Vector3 gravity;
    public float gravityInterval;
    private float startInterval;

    void Start()
    {
        // Set initial gravity value.
        // Not actually used until Update().
        gravity.x = -3;
    }

    /**
     * Every 8s alternate gravity between 2 axis (x, -x).
    */
    void Update()
    {
        // Time interval reached.
        if (System.Math.Abs(startInterval - Time.fixedTime) >= gravityInterval)
        {
            // Invert gravity globally, left -> right, right -> left.
            Physics2D.gravity = gravity = new Vector3(gravity.x * -1, 0, 0);

            // Reset time counter.
            startInterval = Time.fixedTime;
        }
    }

    public void SpawnBall(Vector3 position, Vector3 startingVelocity)
    {
        // Debug.Log("Spawning ball in position " + position.ToString() +
        //    ", startingVelocity " + startingVelocity.ToString());

        var spawned = Instantiate(ball);
        var rigid = spawned.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.position = position;
            rigid.velocity = startingVelocity;
        }
        else
        {
            // Debug.LogWarning("Spawned ball but couldn't find rigidbody component.");
        }
        var ballMovement = spawned.GetComponent<BallMovement>();
        if (ballMovement)
        {
            ballMovement.racquetLeft = racquetLeft;
            ballMovement.racquetRight = racquetRight;
        }
        else
        {
            // Debug.LogWarning("Spawned ball but couldn't find ball movement component.");
        }
    }

    public void DeleteAllBalls()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        Debug.Log("Deleting " + balls.Length + " balls.");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
    }
}
