using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Intro : MonoBehaviour {

    public Text m_oddText;
    public Text m_pongText;
    public Text m_oddPongText;
    public Image m_wormHole;

    public int m_maxFontSize = 220;

    public float m_time; // Measure start of game.
    public float m_openingWait = 5f; // Length in seconds before showing animation.

    private bool m_increaseOdd;
    private bool m_increasePong;

    private bool m_swirlyTitle;
    
    void Start () {
        m_time = Time.fixedTime;

        m_pongText.fontSize = 0;
        m_pongText.enabled = false;

        m_oddText.fontSize = 0;
        m_oddText.enabled = false;

        m_increaseOdd = true;
        m_increasePong = false;

        m_wormHole.enabled = false;
        m_swirlyTitle = false;
    }

    private IEnumerator dramaticPong()
    {
        // Reached max size, now flash small to big for effect.
        yield return new WaitForSeconds(1f);
        m_pongText.fontSize = 10;
        yield return new WaitForSeconds(0.5f);
        m_pongText.fontSize = 80;
        yield return new WaitForSeconds(0.5f);
        m_pongText.fontSize = 30;
        yield return new WaitForSeconds(0.5f);
        m_pongText.fontSize = 100;
        yield return new WaitForSeconds(0.5f);
        m_pongText.fontSize = 20;
        yield return new WaitForSeconds(0.5f);
        m_pongText.fontSize = 150;

        m_pongText.enabled = false;
        m_swirlyTitle = true;
    }

    private IEnumerator dramaticOdd()
    {
        // Reached max size, now flash small to big for effect.
        yield return new WaitForSeconds(1f);
        m_oddText.fontSize = 10;
        yield return new WaitForSeconds(0.5f);
        m_oddText.fontSize = 120;
        yield return new WaitForSeconds(0.5f);
        m_oddText.fontSize = 30;
        yield return new WaitForSeconds(0.5f);
        m_oddText.fontSize = 160;

        m_oddText.enabled = false;
        m_increasePong = true; // Only animate pong when odd is done.
    }

    void Update() {
        // Begin opening animation after waiting.
        if (System.Math.Abs(m_time - Time.fixedTime) >= m_openingWait)
        {
            if (m_increaseOdd) // Gradually increase "'Odd'" font.
            {
                if (m_oddText.fontSize < m_maxFontSize)
                {
                    if (!m_oddText.enabled) {
                        m_oddText.enabled = !m_oddText.enabled;
                    }

                    m_oddText.fontSize++;
                }
                else if (m_oddText.fontSize >= m_maxFontSize)
                {
                    // Reached max size of Odd text.
                    // Play dramatic Odd text animation.
                    StartCoroutine(dramaticOdd());
                    m_increaseOdd = false;
                }
            }
            if (m_increasePong) // Gradually increase "Pong" font.
            {
                if (m_pongText.fontSize < m_maxFontSize)
                {
                    if (!m_pongText.enabled)
                    {
                        m_pongText.enabled = !m_pongText.enabled;
                    }

                    m_pongText.fontSize++;
                }
                else if (m_pongText.fontSize >= m_maxFontSize)
                {
                    // Reached max size of Odd text.
                    // Play dramatic Odd text animation.
                    StartCoroutine(dramaticPong());
                    m_increasePong = false;
                }
            }
            if (m_swirlyTitle)
            {
                m_wormHole.enabled = true;
                // Position odd text left.
                // Position odd text right.
                // Position spinning wormhole between both texts.
                // Rotate text around respecting Z of wormhole?
                // While rotating at the bottom of the screen [press enter to play]

                // Odd and Pong get sucked into the black hole showing the menu?
            }
        }
	}
}
