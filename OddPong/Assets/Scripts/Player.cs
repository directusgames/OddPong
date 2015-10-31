using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public int m_score;
    public Text m_scoreOutput;

    public void reset() {
        m_score = 0;
        m_scoreOutput.text = "0";
    }

    /**
     * Update the score.
     * Updates the class properties and updates
     * the linked Text object.
    */
    public void incrementScore() {
        m_score++;
        m_scoreOutput.text = m_score.ToString();
    }

	// Use this for initialization
	void Start () {
        reset();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
