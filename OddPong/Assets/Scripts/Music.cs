using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource[] soundtrack;
    public AudioSource m_track;
    public bool m_playing;

    void Start()
    {
        m_track = soundtrack[Random.Range(0, soundtrack.Length)];
        Debug.Log(m_track.name);
        if (m_track.isActiveAndEnabled)
        {
            m_track.Play();
        }
        else
        {
            Debug.LogError("Track is disabled.");
        }
    }

    void Update()
    {
        if (m_track && !m_track.isPlaying)
        {
            m_track = soundtrack[Random.Range(0, soundtrack.Length)];
            m_track.Play();
        }
    }
}