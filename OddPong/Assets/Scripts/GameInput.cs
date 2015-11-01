using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject pauseMenu;

    public AudioSource bgMusic;

    private UserInput p1Input;
    private UserInput p2Input;

    public bool paused;

    // Use this for initialization
    void Start()
    {
        paused = false;
        pauseMenu.SetActive(false);
        p1Input = playerOne.GetComponent<UserInput>();
        p2Input = playerTwo.GetComponent<UserInput>();
    }

    void SetPaused(bool shouldPause)
    {
        pauseMenu.SetActive(shouldPause);
        p1Input.enabled = !shouldPause;
        p2Input.enabled = !shouldPause;
        if (shouldPause)
        {
            bgMusic.Pause();
        }
        else
        {
            bgMusic.Play();
        }

        var balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in balls)
        {
            var movement = ball.GetComponent<BallMovement>();
            if (movement)
            {
                if (shouldPause)
                {
                    movement.Freeze();
                }
                else
                {
                    movement.Thaw();
                }
            }
        }

        paused = shouldPause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle paused mode.
            SetPaused(!paused);
        }
        else if (paused && Input.GetKeyDown(KeyCode.Q))
        {
            DoApplicationQuit();
        }
    }

    void DoApplicationQuit()
    {
        // Dirty workaround to in-editor play mode not respecting Application.Quit().
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
