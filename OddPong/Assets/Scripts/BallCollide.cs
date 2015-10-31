using UnityEngine;
using System.Collections;

public class BallCollide : MonoBehaviour {
    public GameObject m_barrierObject; // Get own game tag.
    public BoxCollider2D m_barrier;
    public GameObject m_playerOne;
    public GameObject m_playerTwo;
    public GameObject m_mainCamera;
    private Game m_mainScript;

    // Use this for initialization
    void Start () {
        m_mainScript = m_mainCamera.GetComponent<Game>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D() {
        m_mainScript.playerScores(m_barrierObject.gameObject.tag);
    }
}
