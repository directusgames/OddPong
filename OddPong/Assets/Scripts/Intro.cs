using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class Intro : MonoBehaviour {

    public Text m_selector;
    private Music m_soundTrack;

    public Text m_playText;
    //public Text m_settingsText;
    public Text m_instructionsText;
    
    public GameObject introCanvas, instructionsCanvas;

    public AudioSource m_menuSound;

    private bool m_play;
    //private bool m_settings;
    private bool m_instructions;
    private bool instructionsEnabled;

    void Start () {
        m_play = true;
        m_instructions = false;
        m_soundTrack = GameObject.FindGameObjectWithTag("Soundtrack").GetComponent<Music>();
		//m_settingsText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.005f, true, true);
		m_instructionsText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.005f, true, true);
		instructionsEnabled = false;
    }
    


    void Update() {
        if (Input.GetKey(KeyCode.Return))
        {
            if (m_play) {
                m_soundTrack.rememberSong();
                Application.LoadLevel("Main");
            }
            else if(m_instructions)
            {
				m_soundTrack.rememberSong();
				introCanvas.SetActive(false);
				instructionsCanvas.SetActive(true);
				instructionsEnabled = true;
            }
		} else if (Input.GetKey(KeyCode.DownArrow) && !instructionsEnabled) {
             if (m_play) {
                m_play = false;
                m_instructions = true;
                m_menuSound.Play();
				m_instructionsText.CrossFadeColor(new Color(1f, 1f, 1f), 0.25f, true, true);
                m_playText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.25f, true, true);
            }
        } else if (Input.GetKey(KeyCode.UpArrow) && !instructionsEnabled) {
            if (m_instructions) {
                m_instructions = false;
                m_menuSound.Play();
                m_play = true;
                m_playText.CrossFadeColor(new Color(1f, 1f, 1f), 0.25f, true, true);
				m_instructionsText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.25f, true, true);
            }
        }
        else if(Input.GetKey (KeyCode.Escape))
        {
        	if(instructionsEnabled)
        	{
				instructionsCanvas.SetActive(false);
				introCanvas.SetActive(true);
				instructionsEnabled = false;
				m_playText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.005f, true, true);				
        	}
        }
    }
}
