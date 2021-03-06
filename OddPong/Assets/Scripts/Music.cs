﻿using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public AudioSource[] m_soundtrack;
    public AudioSource m_track;

    public Text m_outputText;
    public float m_textResetX = -850f; // Maximum left;
    public float m_textBeginX = 505f; // Maximum right;
    public string m_outputPrefix = "Track: ";
    public bool m_playOnAwake;
    private bool m_play;

    // An arbitrary value to try and gloss over the skip
    // between scenes. I should check this isn't just in editor though.
    private float m_bsValue = 0.03f;

    public void rememberSong() {
        // Implies tracks will have unique names, otherwise,
        // it might just loaded the closest matching or something.
        PlayerPrefs.SetString("continueTrack", m_track.name);
        PlayerPrefs.SetFloat("continueTime", m_track.time);
    }

    private void selectPrevious() {
        m_track = GameObject.Find(PlayerPrefs.GetString("continueTrack")).GetComponent<AudioSource>();
        m_track.time = PlayerPrefs.GetFloat("continueTime") + m_bsValue;
        playTrack(); // Start playing specific song at specific time.
        PlayerPrefs.SetString("continueTrack", "");
        PlayerPrefs.SetFloat("continueTime", 0);
    }

    /**
     *  Play a random song.
     *  Checks whether or not we should continue
     *  playing the existing song ptr at a specific time.
     */
    private void selectRandom() {
        m_track = m_soundtrack[Random.Range(0, m_soundtrack.Length)];
        playTrack();
    }

    private void playTrack() {
        // If playable.
        if (m_track.isActiveAndEnabled) {
            m_outputText.text = m_outputPrefix + m_track.name;
            m_track.Play();
        } else {
            m_outputText.text = "Error loading file: " + m_track.name;
        }
        // Show text with song/track name or with error.
        m_outputText.enabled = true;
    }

    void Start() {
        // Ensure we get a chance to clear the text before displaying.
        m_outputText.enabled = false;

        // Play on awake / start.
        if (m_playOnAwake) {
            m_play = true;

            // Resume where we left off, elsewhere (i.e. intro scene).
            if (PlayerPrefs.GetFloat("continueTime") > 0) {
                selectPrevious();
            } else { // Else choose a random song.
                selectRandom(); // Start playing a random song.
            }
        } else {
            m_play = false;
        }
    }

    void Update()
    {
        if (m_play && m_track && !m_track.isPlaying) {
            selectRandom();
        }
        // Animate playing text.
        if (m_play && m_track && m_track.isPlaying) {
            // Gone too far out, reset text position to right handside.
            if (m_outputText.transform.localPosition.x < m_textResetX) {
                m_outputText.transform.localPosition = new Vector3(
                    m_textBeginX, 
                    m_outputText.transform.localPosition.y,
                    m_outputText.transform.localPosition.z
                );
            } else {
                m_outputText.transform.localPosition = new Vector3(
                    m_outputText.transform.localPosition.x - 1,
                    m_outputText.transform.localPosition.y,
                    m_outputText.transform.localPosition.z
                );
            }
        }
    }
}