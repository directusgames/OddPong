using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    GravityManip m_gravityManipulator;

    public bool m_triggerGravity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (m_triggerGravity)
        {
            m_gravityManipulator.PlayerGravity();
            m_triggerGravity = false;
        }
	}
}
