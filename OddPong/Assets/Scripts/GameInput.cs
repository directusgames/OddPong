using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;

    public AudioSource bgMusic;

    private UserInput p1Input;
    private UserInput p2Input;

    public bool paused;

    // Use this for initialization
    void Start()
    {
        paused = false;
        p1Input = playerOne.GetComponent<UserInput>();
        p2Input = playerTwo.GetComponent<UserInput>();
    }

    void Pause()
    {
        if (!paused) // Pause.
        {
            p1Input.enabled = false;
            p2Input.enabled = false;
            bgMusic.Pause();

            var balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var ball in balls)
            {
                var movement = ball.GetComponent<BallMovement>();
                if (movement)
                {
                    movement.Freeze();
                }
            }

            paused = true;
        }
        else // Un-pause.
        {
            p1Input.enabled = true;
            p2Input.enabled = true;
            bgMusic.Play();

            var balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (var ball in balls)
            {
                var movement = ball.GetComponent<BallMovement>();
                if (movement)
                {
                    movement.Thaw();
                }
            }

            paused = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO:
            // - 1) Pause.
            // - 2) Confirm with user.
            // - 3) If cancel, un-pause, resume.
            // - 4) If confirm, Application.Quit().
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }
}
