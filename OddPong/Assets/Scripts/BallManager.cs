using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject racquetLeft;
    public GameObject racquetRight;

    public bool m_gravityApplies;
    public float m_gravityScale = 20f;
    

    public void SpawnBall(Vector3 position, Vector3 startingVelocity)
    {
        var spawned = Instantiate(ball);
        var rigid = spawned.GetComponent<Rigidbody2D>();
        if (rigid)
        {
            rigid.position = position;
            rigid.velocity = startingVelocity;
            rigid.gravityScale = m_gravityApplies ? m_gravityScale : 0f;
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

    // Balls that don't heed gravity.
    public void AntiGravBalls()
    {
        m_gravityApplies = false;
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        // Debug.Log("Deleting " + balls.Length + " balls.");
        foreach (var ball in balls)
        {
            ball.GetComponent<Rigidbody2D>().gravityScale = 0;
            ball.GetComponent<BallMovement>().GetUpToSpeed();
        }
    }

    // Balls that heed gravity.
    public void GravBalls()
    {
        m_gravityApplies = true;
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            ball.GetComponent<Rigidbody2D>().gravityScale = m_gravityScale;
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
