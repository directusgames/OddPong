using UnityEngine;
using System.Collections;

public class GameInput : MonoBehaviour
{
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject pauseMenu;

    public AudioSource bgMusic;
    public AudioSource pauseSound;
    public AudioSource unpauseSound;

    private UserInput p1Input;
    private UserInput p2Input;
    
    private GameObject[] soundList;


    public bool showingPauseMenu;

    // Use this for initialization
    void Start()
    {
        showingPauseMenu = false;
        pauseMenu.SetActive(false);
        p1Input = playerOne.GetComponent<UserInput>();
        p2Input = playerTwo.GetComponent<UserInput>();
        soundList = GameObject.FindGameObjectsWithTag("Pausable Sound");        

    }

    void SetPaused(bool shouldPause)
    {
        // Pause all movement and timers.
        Time.timeScale = (shouldPause) ? 0.0f : 1.0f;
        if (shouldPause)
        {
            pauseSound.Play();
            
            foreach(GameObject sound in soundList)
            {
            	sound.GetComponent<AudioSource>().Pause();
            }
            
        }
        else
        {
            unpauseSound.Play();
            
			foreach(GameObject sound in soundList)
			{
				sound.GetComponent<AudioSource>().Play();
			}
        }

        pauseMenu.SetActive(shouldPause);
        p1Input.enabled = !shouldPause;
        p2Input.enabled = !shouldPause;

        showingPauseMenu = shouldPause;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Toggle paused mode.
            SetPaused(!showingPauseMenu);
        }
        if (showingPauseMenu)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                DoApplicationQuit();
            }
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
