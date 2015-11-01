using UnityEngine;
using System.Collections;

public class GravityManip : MonoBehaviour {
    public Vector2 m_gravity;

    public BallManager m_ballManager;

    // Left and right strong vectors.
    // Forces player to play 'keepuppy' with themselves.
    private Vector2 m_strongLeft = new Vector2(-40, 0);
    private Vector2 m_strongRight = new Vector2(40, 0);

    public bool m_swapPlayer;
    public float m_intervalTime; // Gravity interval in seconds.
    public float m_prevTime; // Start time to measure against current time.

    // WIP / broken:
    // private Vector2 slimeSoccerUp = new Vector2(0, 10);
    // private Vector2 slimeSoccerDown = new Vector2(0, -10);

    public void NormalGravity()
    {
        m_ballManager.AntiGravBalls();
    }

    private void SwapPlayer()
    {
        m_swapPlayer = false;
        // Invert vector function instead?
        m_gravity = m_gravity.Equals(m_strongLeft) ? m_strongRight : m_strongLeft;
        m_prevTime = Time.fixedTime;
        Physics2D.gravity = m_gravity;
    }

    public void PlayerGravity()
    {
        m_ballManager.GravBalls();
        m_swapPlayer = true; // Next iteration we will want to swap the player.

        // Randomise the pain.
        int rand = Random.Range(0, 1);
        m_gravity = rand == 1 ? m_strongLeft : m_strongRight;
        m_prevTime = Time.fixedTime;
        Physics2D.gravity = m_gravity;
    }

    void Start()
    {
        m_swapPlayer = false;
    }

    void Update()
    {
        if (m_swapPlayer)
        {
            if ((System.Math.Abs(m_prevTime - Time.fixedTime)) >= m_intervalTime)
            {
                SwapPlayer();
            }
        }
    }
}
