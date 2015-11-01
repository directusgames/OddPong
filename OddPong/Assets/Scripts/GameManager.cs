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
    public Text m_textOutput;

    // A match is composed of rounds.
    public bool m_matchCooldown; // Whether in cool down state.
    public float m_matchCooldownTime; // Length of game cool down between games.
    private float m_matchCooldownItr; // Time cool down begun.

    public bool m_roundCooldown; // Whether in cool down state.
    public float m_roundCooldownTime; // Length of game cool down between games.
    private float m_roundCooldownItr; // Time cool down begun.

    void resetGame()
    {
        // Reset player scores.
        p1Controller.reset();
        p2Controller.reset();
        m_textOutput.text = "";
        DoBallSpawn();
    }

    void resetRound()
    {
        m_textOutput.text = "";
        DoBallSpawn();
    }

    void Start()
    {
        p1Controller = m_playerOne.GetComponent<Player>();
        p2Controller = m_playerTwo.GetComponent<Player>();
        resetGame();
    }
    
    public void playerScores(string player)
    {
        Player scorer = (GameObject.FindGameObjectWithTag(player)).GetComponent<Player>();
        scorer.incrementScore();
        m_ballManager.DeleteAllBalls();

        // If the match has concluded based on score (in which case there is no
        // need to use the round cooldown logic.
        if (p1Controller.m_score >= m_maxScore || p2Controller.m_score >= m_maxScore)
        {
            m_textOutput.text = player + " wins!";
            m_matchCooldownItr = Time.fixedTime;
            m_matchCooldown = true;
        } else 
        {
            m_textOutput.text = player + " scores!";
            m_roundCooldownItr = Time.fixedTime;
            m_roundCooldown = true;
        }
    }

    void DoBallSpawn()
    {
		//Either 'right' or 'left'
		int[] xDir = new int[2];
		xDir[0] = -1;
		xDir[1] = 1;
		
    	//For the X it chooses either 1 or -1, and for y it will be a random float between -1 and 1, this should
    	//give us the correct 'diagonal' angle
    	Vector2 initialBallDir = new Vector2(xDir[Random.Range (0,xDir.Length)], Random.Range (-1f,1f));
    	
		float camHeight = Camera.main.orthographicSize/2;
    	
    	//Debug.Log ("Camera height: " + Camera.main.orthographicSize);
		Vector3 spawnPos = new Vector3(0,Random.Range (camHeight, -camHeight), 0);
        Vector3 startingVelocity = initialBallDir * m_initialBallSpeed;
        m_ballManager.SpawnBall(spawnPos, startingVelocity);
    }

    // Core Game Loop
    void Update()
    {
        if (!m_matchCooldown) // Match is ongoing.
        {
            if (m_roundCooldown) // Round has completed. Cool off.
            {
                var timeDiff = System.Math.Abs(m_roundCooldownItr - Time.fixedTime);
                if (timeDiff >= m_roundCooldownTime) // Cooldown time expired.
                {
                    resetRound();
                    m_roundCooldown = false;
                }
            }
        }
        else  // Match has completed. Cool off.
        {
            var timeDiff = System.Math.Abs(m_matchCooldownItr - Time.fixedTime);
            if (timeDiff >= m_matchCooldownTime) // Cooldown time expired.
            {
                resetGame();
                m_matchCooldown = false;
            }
        }
    }
}
