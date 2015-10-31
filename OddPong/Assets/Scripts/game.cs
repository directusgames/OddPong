using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class game : MonoBehaviour {
    public BoxCollider2D leftBarrier;
    public BoxCollider2D rightBarrier;

    public Transform ballPos;
    public Rigidbody2D ballBody;

    public Text player1Score;
    public Text player2Score;

    // Use this for initialization
    void Start () {
        player1Score.text = "0";
        player2Score.text = "0";
    }
	
	// Update is called once per frame
	void Update () {
        if (ballBody.IsTouching(leftBarrier)) {
            // Player 2 scores.
            player2Score.text = (int.Parse(player2Score.text) + 1).ToString();
            ballPos.position = new Vector2(0f, 0f);

        } else if (ballBody.IsTouching(rightBarrier)) {
            // Player 1 scores.
            player1Score.text = (int.Parse(player1Score.text) + 1).ToString();
            ballPos.position = new Vector2(0f, 0f);
        }
	}
}
