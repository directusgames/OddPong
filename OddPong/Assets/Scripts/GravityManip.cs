﻿using UnityEngine;

public class GravityManip : MonoBehaviour, GameEvent  {
    public Vector2 m_gravity;
    public BallManager m_ballManager;

    // Left and right strong vectors.
    // Forces player to play 'keepuppy' with themselves.
    private Vector2 m_strongLeft = new Vector2(-40, 0);
    private Vector2 m_strongRight = new Vector2(40, 0);

    public float m_animateTime; // Gravity interval in seconds.
    public float m_prevTime; // Start time to measure against current time.

    public bool m_running;

    // WIP / broken:
    // private Vector2 slimeSoccerUp = new Vector2(0, 10);
    // private Vector2 slimeSoccerDown = new Vector2(0, -10);

    // Return to normal.
    public void StopRandomEvent()
    {
        Debug.Log("stoprandomevent");
        // Apply normal gravity.
        m_ballManager.AntiGravBalls();
        Physics2D.gravity = new Vector2(0f, 0f);
        m_running = false;
    }

    // Target Player 1.
    public void StartRandomEvent()
    {
        m_ballManager.GravBalls(); // Allow ball to experience gravity.
        // Randomise intial side of gravity.
        float rand = Random.Range(0.0f, 1.0f);
        m_gravity = rand <= 0.4f ? m_strongLeft : m_strongRight;
        Physics2D.gravity = m_gravity;
        m_prevTime = Time.fixedTime;
        m_running = true;
    }

    void Start()
    {
    }

    void Update()
    {
        if (m_running)
        {
            var timeDiff = System.Math.Abs(m_prevTime - Time.fixedTime);
            if (timeDiff > m_animateTime)
            {
                StopRandomEvent();
            }
        }
    }
}
