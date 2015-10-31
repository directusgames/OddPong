using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ball;

    public void SpawnBall(Vector3 position, Vector3 startingVelocity)
    {
        Debug.Log("Spawning ball in position " + position.ToString() +
            ", startingVelocity " + startingVelocity.ToString());

        var spawned = Instantiate(ball);
        spawned.tag = "Ball"; // TODO: This should be on the prefab.
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
