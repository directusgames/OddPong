﻿using UnityEngine;
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
    
    private GameObject eventMgr;

    public BallManager m_ballManager;
    public float m_initialBallSpeed;
    
    public float roundTime;
    public float ballSpeedIncreaseTime; //How long a round should go for until the ball starts increasing in speed
    public float ballSpeedIncreaseInterval; //How long between increases
    public float intervalTime;
    public float ballSpeedIncreaseAmount; //How much to increase each time
    
    private bool warningGiven;
    private GameObject alertMgr;

    public int m_maxScore;
    public Text m_countdown;
    public Text m_winner;

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
        
        //Has an alert been sounded for ball increase?
        warningGiven = false;
        
        alertMgr = GameObject.Find ("AlertManager");
        eventMgr = GameObject.Find ("Eventmanager");
        m_matchCooldownPrev = Time.fixedTime;
    }

    void startGame()
    {
        p1Controller.reset();
        p2Controller.reset();
        m_countdown.text = "";
        m_winner.text = "";
        DoBallSpawn(); // Spawn ball after count down.
        roundTime = 0;
    }

    void startRound()
    {
        m_countdown.text = "";
        m_winner.text = "";
        DoBallSpawn();
        roundTime = 0;
        warningGiven = false;
        
    }

    public void playerScores(string player)
    {
        Player scorer = GameObject.FindGameObjectWithTag(player).GetComponent<Player>();
        scorer.incrementScore();
        m_ballManager.DeleteAllBalls();
        m_lastWinnerPosition = scorer.transform.position;

        if (p1Controller.m_score >= m_maxScore || p2Controller.m_score >= m_maxScore)
        {
            m_winner.text = player + " wins!";
            m_matchCooldownPrev = Time.fixedTime;
            m_matchCooldown = true;
            soundWinGame.Play();
        }
        else
        {
            m_winner.text = player + " scores!";
            m_roundCooldownPrev = Time.fixedTime;
            m_roundCooldown = true;
            soundWinRound.Play();
        }
        
        eventMgr.GetComponent<EventManager>().StopAllEvents();
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
        	roundTime = roundTime + Time.deltaTime;
        	
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
            m_countdown.enabled = true;
            if (matchDiff >= m_matchCooldownLength)
            {
                m_countdown.enabled = false;
                startGame();
                m_matchCooldown = false;
                m_startGame = false;
            } else { // The time interval is less than the required.
                m_countdown.text = ((int) (m_matchCooldownLength - matchDiff) + 1).ToString();
            }
        }
        
        if(roundTime > ballSpeedIncreaseTime)
        {
        	if(!warningGiven)
        	{
				alertMgr.GetComponent<AlertManager>().ShowAlert("BALL SPEED INCREASE");
				warningGiven = true;
        	}
        	
        	if(intervalTime > ballSpeedIncreaseInterval)
        	{
        		intervalTime = 0;
        		GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        		
        		foreach(GameObject ball in balls)
        		{
        			ball.GetComponent<BallMovement>().ballSpeed += ballSpeedIncreaseAmount;
        		}
        		
        	}
        	
      		intervalTime += Time.deltaTime;			      	
        }
        
        roundTime += Time.deltaTime;
    }    

}
