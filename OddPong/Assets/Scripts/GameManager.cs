using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject m_playerOne;
    public GameObject m_playerTwo;

    public AudioSource soundWinGame;
    public AudioSource soundWinRound;

    private Player p1Controller;
    private Player p2Controller;

    public BallManager m_ballManager;
    public float m_initialBallSpeed;

    public int m_maxScore;
    public Text m_outputText;

    // A match consists of multiple rounds (defined by m_maxScore).
    public bool m_matchCooldown;
    public float m_matchCooldownLength;
    private float m_matchCooldownPrev;
    public bool m_roundCooldown;
    public float m_roundCooldownLength;
    private float m_roundCooldownPrev;

    void Start()
    {
        p1Controller = m_playerOne.GetComponent<Player>();
        p2Controller = m_playerTwo.GetComponent<Player>();
        resetGame();
    }

    void resetGame()
    {
        p1Controller.reset();
        p2Controller.reset();
        m_outputText.text = "";
        DoBallSpawn();
    }

    void resetRound()
    {
        m_outputText.text = "";
        DoBallSpawn();
    }

    public void playerScores(string player)
    {
        Player scorer = GameObject.FindGameObjectWithTag(player).GetComponent<Player>();
        scorer.incrementScore();
        m_ballManager.DeleteAllBalls();
        
        if (p1Controller.m_score >= m_maxScore || p2Controller.m_score >= m_maxScore)
        {
            m_outputText.text = player + " wins!";
            m_matchCooldownPrev = Time.fixedTime;
            m_matchCooldown = true;
            soundWinGame.Play();
        }
        else
        {
            m_outputText.text = player + " scores!";
            m_roundCooldownPrev = Time.fixedTime;
            m_roundCooldown = true;
            soundWinRound.Play();
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

    // Update is called once per frame
    void Update()
    {
        if (!m_matchCooldown) // Game is ongoing.
        {
            if (m_roundCooldown) // Round is in cooldown.
            {
                var roundDiff = System.Math.Abs(m_roundCooldownPrev - Time.fixedTime);
                if (roundDiff >= m_roundCooldownLength)
                {
                    resetRound();
                    m_roundCooldown = false;
                }
            }
        } else // Game is in cooldown.
        {
            var matchDiff = System.Math.Abs(m_matchCooldownPrev - Time.fixedTime);
            if (matchDiff >= m_matchCooldownLength)
            {
                resetGame();
                m_matchCooldown = false;
            }
        }
    }
}
