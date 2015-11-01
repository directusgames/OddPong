using UnityEngine;

public class GravityManip : MonoBehaviour, GameEvent  {
    public Vector2 m_gravity;
    public BallManager m_ballManager;
    public FadingAudioSource m_gravitySound;
    public GameObject gravEffectLeft, gravEffectRight;

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
        
        if(m_gravity == m_strongLeft)
        {
			gravEffectLeft.GetComponent<GravityEffectController>().EffectOff ();
        }
        else
        {
			gravEffectRight.GetComponent<GravityEffectController>().EffectOff ();
        }
        
        Physics2D.gravity = new Vector2(0f, 0f);
        m_gravitySound.FadeOut();
        m_running = false;
    }

    // Target random player with gravity.
    public void StartRandomEvent()
    {
        m_ballManager.GravBalls(); // Allow ball to experience gravity.
        // Randomise initial side of gravity.
        float rand = Random.Range(0.0f, 1.0f);
        if (rand <= 0.4f) {
	    	m_gravity = m_strongLeft;
	    	gravEffectLeft.GetComponent<GravityEffectController>().EffectOn ();
    	} else {
    		m_gravity = m_strongRight;
			gravEffectRight.GetComponent<GravityEffectController>().EffectOn ();
    	}
        
        Physics2D.gravity = m_gravity;
        m_prevTime = Time.fixedTime;
        m_gravitySound.Play();
        m_running = true;
    }

    void Start()
    {
        m_gravitySound.FadeTime = m_animateTime;
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
