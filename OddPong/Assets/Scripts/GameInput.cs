using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour {
    public GameObject playerOne;
    public GameObject playerTwo;

    public GameObject ball;
    private Rigidbody2D ballBody;
    private BallMovement ballScript;
    private Vector3 tempVeloc;
    private float tempAngular;

    public AudioSource bgMusic;

    private UserInput p1Input;
    private UserInput p2Input;

    public bool paused;

	// Use this for initialization
	void Start () {
        paused = false;
        p1Input = playerOne.GetComponent<UserInput>();
        p2Input = playerTwo.GetComponent<UserInput>();
        ballScript = ball.GetComponent<BallMovement>();
        ballBody = ball.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Pause.
            if (!paused) {
                p1Input.enabled = false;
                p2Input.enabled = false;
                bgMusic.Pause();
                ballScript.enabled = false;
                tempVeloc = ballBody.velocity;
                tempAngular = ballBody.angularVelocity;
                ballBody.isKinematic = true;
                paused = true;
            } else { // Un-pause.
                p1Input.enabled = true;
                p2Input.enabled = true;
                bgMusic.Play();
                ballScript.enabled = true;
                ballBody.isKinematic = false;
                ballBody.velocity = tempVeloc;
                ballBody.angularVelocity = tempAngular;
                paused = false;
            }
        }
	}
}
