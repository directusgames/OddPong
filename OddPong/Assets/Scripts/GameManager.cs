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

    public bool m_startGame;

    // A match consists of multiple rounds (defined by m_maxScore).
    public bool m_matchCooldown;
    public float m_matchCooldownLength;
    private float m_matchCooldownPrev;

    public bool m_roundCooldown;
    public float m_roundCooldownLength;
    private float m_roundCooldownPrev;

    private Vector3 m_lastWinnerPosition;

    void Start()
    {
        m_matchCooldown = true;
        m_startGame = true;
        p1Controller = m_playerOne.GetComponent<Player>();
        p2Controller = m_playerTwo.GetComponent<Player>();

        // Randomly pick who was the previous 'winner' so we have a starting point.
        m_lastWinnerPosition = (Random.Range(0.0f, 1.0f) >= 0.5f) ? p1Controller.transform.position : p2Controller.transform.position;
    }

    void startGame()
    {
        p1Controller.reset();
        p2Controller.reset();
        m_outputText.text = "";
        DoBallSpawn(); // Spawn ball after count down.
    }

    void startRound()
    {
        m_outputText.text = "";
        DoBallSpawn();
    }

    public void playerScores(string player)
    {
        Player scorer = GameObject.FindGameObjectWithTag(player).GetComponent<Player>();
        scorer.incrementScore();
        m_ballManager.DeleteAllBalls();
        m_lastWinnerPosition = scorer.transform.position;

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
        // For X, make the ball go to the previous winner.
        // For Y, randomly pick a direction (angle).
        float spawnDirection = (this.transform.position.x >= m_lastWinnerPosition.x) ? -1.0f : 1.0f;
        Vector2 initialBallDir = new Vector2(spawnDirection, Random.Range(-1.0f, 1.0f));

        float camHeight = Camera.main.orthographicSize / 2.0f;
        Vector3 spawnPos = new Vector3(0, Random.Range(camHeight, -camHeight), 0);
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
                    startRound();
                    m_roundCooldown = false;
                }
            }
        }
        else // Game is in cooldown.
        {
            var matchDiff = System.Math.Abs(m_matchCooldownPrev - Time.fixedTime);
            m_outputText.enabled = true;
            if (matchDiff >= m_matchCooldownLength)
            {
                startGame();
                m_matchCooldown = false;
                m_startGame = false;
            }
        }
    }
}
