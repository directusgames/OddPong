using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public GameManager m_gameManager;
    public GravityManip m_gravityManipulator;

    public float m_gravityInterval;
    public float m_gravityPrev;

    // The round time before using an effect.
    public float m_timeBeforeEffects = 10f;
    public float m_effectTimePrev;

	void Start () {
        m_effectTimePrev = Time.fixedDeltaTime;
    }

    void Update () {
        /*  Match is between matches, or between rounds.
            Setting the times each iteration is a bit confusing in my oppinion,
            It is only on the last iteration that these values going to be used.
        */
        bool noEffectState = m_gameManager.m_startGame || m_gameManager.m_roundCooldown || m_gameManager.m_matchCooldown;
        if (noEffectState)
        {
            m_effectTimePrev = Time.fixedTime;

            // Update custom event times - to increase the time that should be waiteed.
            m_gravityPrev = Time.fixedDeltaTime;
        } else // Match is in progress.
        {
            // If the round has been going long enough for effects to start.
            var roundDiff = System.Math.Abs(m_effectTimePrev - Time.fixedTime);

            // Events are now allowed.
            if (roundDiff > m_timeBeforeEffects)
            {
                // Check individual events time intervals to see if an event should fire.
                var gravityDiff = System.Math.Abs(m_gravityPrev - Time.fixedTime);
                if (gravityDiff >= m_gravityInterval ) {
                    // Note, this effects length is determined by GravityManipulator.
                    m_gravityManipulator.PlayerGravity();
                    m_gravityPrev = Time.fixedTime;
                }
            }
        }
    }
}

