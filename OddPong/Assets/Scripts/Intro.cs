using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class Intro : MonoBehaviour {

    public Text m_selector;
    private Music m_soundTrack;

    public Text m_playText;
    public Text m_settingsText;

    public AudioSource m_menuSound;

    private bool m_play;
    private bool m_settings;

    void Start () {
        m_play = true;
        m_settings = false;
        m_soundTrack = GameObject.FindGameObjectWithTag("Soundtrack").GetComponent<Music>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.Return))
        {
            if (m_play) {
                m_soundTrack.rememberSong();
                Application.LoadLevel("Main");
            }
        } else if (Input.GetKey(KeyCode.DownArrow)) {
             if (m_play) {
                m_play = false;
                m_settings = true;
                m_menuSound.Play();
                m_settingsText.color = new Color(1f, 1f, 1f);
                m_playText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.25f, true, true);
            }
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            if (m_settings) {
                m_settings = false;
                m_menuSound.Play();
                m_play = true;
                m_playText.CrossFadeColor(new Color(1f, 1f, 1f), 0.25f, true, true);
                m_settingsText.color = new Color(39f / 255f, 39f / 255f, 39f / 255f);
            }
        }
    }
}
