using UnityEngine;

public class MultiballerController : MonoBehaviour
{
    public int numBalls;
    public float ballSpeed;
    public float spawnDelaySeconds;
    public GameObject attractor;
    public BallManager ballManager;


    // Use this for initialization
    void Start()
    {
        ballManager = GameObject.FindObjectOfType<BallManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Ball")
        {
            Debug.Log("Ball entered inner collider");
            Destroy(coll.gameObject);

            SetUpMultiBallSpawn();
        }
    }

    void SetUpMultiBallSpawn()
    {
        //Disable attractor
        attractor.SetActive(false);

        //Disable collider
        GetComponent<CircleCollider2D>().enabled = false;

        transform.parent.gameObject.GetComponent<BlackHoleController>().ScaleDown();

        // Actually spawn the balls with some delay.
        Invoke("DoMultiBallSpawn", spawnDelaySeconds);
    }

    void DoMultiBallSpawn()
    {
        for (int ballNum = 1; ballNum <= numBalls; ballNum++)
        {
            // Alternate between sending balls left and right, at a random angle.
            float ballDirection = (ballNum % 2 != 0) ? -1.0f : 1.0f;
            ballManager.SpawnBall(transform.position, new Vector3(ballDirection, Random.Range(-1f, 1f), 0) * ballSpeed);
        }
    }
}
