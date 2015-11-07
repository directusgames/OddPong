using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class Intro : MonoBehaviour {

    public Text m_playText;
    public Text m_settingsText;

    private bool m_play;
    private bool m_settings;

    void Start () {
        m_play = true;
        m_settings = false;
    }

    void Update() {
        if (Input.GetKey(KeyCode.Return))
        {
            if (m_play) {
                Application.LoadLevel("Main");
            }
        } else if (Input.GetKey(KeyCode.DownArrow)) {
             if (m_play) {
                m_play = false;
                m_settings = true;
                m_settingsText.color = new Color(1f, 1f, 1f);
                m_playText.CrossFadeColor(new Color(39f/255f, 39f/255f, 39f/255f), 0.25f, true, true);
            }
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            if (m_settings) {
                m_settings = false;
                m_play = true;
                m_playText.CrossFadeColor(new Color(1f, 1f, 1f), 0.25f, true, true);
                m_settingsText.color = new Color(39f / 255f, 39f / 255f, 39f / 255f);
            }
        }
	}
}
