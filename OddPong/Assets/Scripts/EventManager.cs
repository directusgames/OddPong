using UnityEngine;
using System.Collections.Generic;

public class EventManager : MonoBehaviour {
    public GameManager m_gameManager;
    public GravityManip m_gravityManipulator;

    // Private GameEvent array of all random events.
    private List<GameEvent> m_randomEvents;
    private GameObject[] gameSounds;

    // The round time before using an effect.
    public float m_timeBeforeEffects = 10f;
    public float m_effectTimePrev;

    public float m_eventCooldown = 5f;
    public float m_eventTimePrev;

	void Start () {
        // Populate events.
        m_randomEvents = new List<GameEvent>();
        var matches = GameObject.FindGameObjectsWithTag("RandomEvent");
        foreach (var match in matches)
        {
            m_randomEvents.Add(match.GetComponent<GameEvent>());
        }
        m_effectTimePrev = Time.fixedTime;
        
        gameSounds = GameObject.FindGameObjectsWithTag("Pausable Sound");
    }

    void Update () {
        /*  Match is between matches, or between rounds.
            Setting the time on each iteration is a bit confusing in my oppinion,
            It is only on the last iteration that these values going to be used.
        */
        bool noEffectState = m_gameManager.m_startGame || m_gameManager.m_roundCooldown || m_gameManager.m_matchCooldown;
        if (noEffectState)
        {
            m_effectTimePrev = Time.fixedTime;
            m_eventTimePrev = Time.fixedTime;
        } else // Match is in progress.
        {
            // If the round has been going long enough for effects to start.
            var roundDiff = System.Math.Abs(m_effectTimePrev - Time.fixedTime);

            // Events are now allowed.
            if (roundDiff > m_timeBeforeEffects)
            {
                var eventDiff = System.Math.Abs(m_eventTimePrev - Time.fixedTime);

                // Event cooldown has finished.
                if (eventDiff > m_eventCooldown)
                {
                    Debug.Log("event cooldown finished");

                    // Randomly choose an event to fire.
                    int rand = Random.Range(0, m_randomEvents.Count);
                    m_randomEvents[rand].StartRandomEvent();
                    m_eventTimePrev = Time.fixedTime;
                }
            }
        }
    }
    
    public void StopAllEvents()
    {
    	foreach(GameEvent ge in m_randomEvents)
    	{
    		ge.StopRandomEvent();
    	}
    	
    	foreach(GameObject sound in gameSounds)
    	{
    		sound.GetComponent<AudioSource>().Stop ();
    	} 	
    }
}

