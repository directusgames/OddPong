using UnityEngine;
using System.Collections;

public class BallCollide : MonoBehaviour
{
    public GameManager m_mainScript;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D()
    {
        m_mainScript.playerScores(this.gameObject.tag);
    }
}
