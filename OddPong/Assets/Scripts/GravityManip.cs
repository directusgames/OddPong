using UnityEngine;
using System.Collections;

public class GravityManip : MonoBehaviour {
    public Vector2 m_gravity;

    public BallManager m_ballManager;

    // Left and right strong vectors.
    // Forces player to play 'keepuppy' with themselves.
    private Vector2 m_strongLeft = new Vector2(-40, 0);
    private Vector2 m_strongRight = new Vector2(40, 0);

    public bool m_swapNormal;
    // Uses m_swapTime to decide when to go back to normal.
    public float m_normalPrevTime;

    public bool m_swapPlayer;
    public float m_swapTime; // Gravity interval in seconds.
    public float m_swapPrevTime; // Start time to measure against current time.

    // WIP / broken:
    // private Vector2 slimeSoccerUp = new Vector2(0, 10);
    // private Vector2 slimeSoccerDown = new Vector2(0, -10);

    public void NormalGravity()
    {
        Debug.Log("Normal gravity");
        m_ballManager.AntiGravBalls();
        m_swapNormal = false;
    }

    private void SwapPlayer()
    {
        Debug.Log("Swap player");
        // Invert vector function instead?
        m_gravity = m_gravity.Equals(m_strongLeft) ? m_strongRight : m_strongLeft;
        Physics2D.gravity = m_gravity;

        // No longer want to swap player.
        m_swapPlayer = false;

        // We will want to revert back to normal after the same interval time.
        m_swapNormal = true;
        m_normalPrevTime = Time.fixedTime;
    }

    public void PlayerGravity()
    {
        Debug.Log("Player gravity");
        m_ballManager.GravBalls(); // Allow ball to experience gravity.

        // Randomise the pain.
        int rand = Random.Range(0, 1);
        m_gravity = rand == 1 ? m_strongLeft : m_strongRight;

        Physics2D.gravity = m_gravity;

        m_swapPlayer = true; // Next iteration we will want to swap the player.
        m_swapPrevTime = Time.fixedTime;
    }

    void Start()
    {
        m_swapPlayer = false;
        m_swapNormal = false;
    }

    void Update()
    {
        if (m_swapPlayer)
        {
            var playerSwapDiff = System.Math.Abs(m_swapPrevTime - Time.fixedTime);
            if (playerSwapDiff >= m_swapTime)
            {
                Debug.Log("Calling swap player");
                SwapPlayer();
            }
        }
        if (m_swapNormal)
        {
            var normSwapDiff = System.Math.Abs(m_normalPrevTime - Time.fixedTime);
            if (normSwapDiff >= m_swapTime)
            {
                Debug.Log("Calling normal gravity");
                NormalGravity();
            }
            
        }
    }
}
