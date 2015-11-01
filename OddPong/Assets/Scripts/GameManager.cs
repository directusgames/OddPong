using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject m_playerOne;
    public GameObject m_playerTwo;

    private Player p1Controller;
    private Player p2Controller;

    public BallManager m_ballManager;
    public float m_initialBallSpeed;

    public int m_maxScore;
    public Text m_winnerText;

    public bool m_coolDown; // Whether in cool down state.
    public float m_cooldownTime; // Length of game cool down between rounds.
    private float m_cooldownStart; // Time cool down begun.

    void Start()
    {
        p1Controller = m_playerOne.GetComponent<Player>();
        p2Controller = m_playerTwo.GetComponent<Player>();
        resetGame();
    }

    void resetGame()
    {
        // Reset player scores.
        p1Controller.reset();
        p2Controller.reset();

        m_winnerText.text = "";

        DoBallSpawn();
        m_coolDown = false;
    }

    public void playerScores(string player)
    {
        Debug.Log("Player has scored: " + player);
        // Could be better.
        if (player.Equals("PlayerOne"))
        {
            p1Controller.incrementScore();
        }
        else if (player.Equals("PlayerTwo"))
        {
            p2Controller.incrementScore();
        }
        else
        {
            Debug.LogError("Warning player scores with invalid string.");
        }

        m_ballManager.DeleteAllBalls();
        if (p1Controller.m_score >= m_maxScore || p2Controller.m_score >= m_maxScore)
        {
            m_winnerText.text = player + " wins!";
            m_cooldownStart = Time.fixedTime;
            m_coolDown = true;
        }
        else
        {
            DoBallSpawn();
        }
    }

    void DoBallSpawn()
    {
        Vector3 spawnPos = new Vector3();
        Vector3 startingVelocity = Vector2.right * m_initialBallSpeed;
        m_ballManager.SpawnBall(spawnPos, startingVelocity);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_coolDown)
        {
            if (System.Math.Abs(m_cooldownStart - Time.fixedTime) >= m_cooldownTime)
            {
                resetGame();
            }
        }
    }
}
