using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject racquetLeft;
    public GameObject racquetRight;

    public void SpawnBall(Vector3 position, Vector3 startingVelocity)
    {
        var spawned = Instantiate(ball);
        var rigid = spawned.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.position = position;
            rigid.velocity = startingVelocity;
        }
        else
        {
            Debug.LogWarning("Spawned ball but couldn't find rigidbody component.");
        }
        var ballMovement = spawned.GetComponent<BallMovement>();
        if (ballMovement)
        {
            ballMovement.racquetLeft = racquetLeft;
            ballMovement.racquetRight = racquetRight;
        }
        else
        {
            Debug.LogWarning("Spawned ball but couldn't find ball movement component.");
        }
    }

    public void DeleteAllBalls()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            Destroy(ball);
        }
    }
}
