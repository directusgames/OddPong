using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BlackholeTwirl : MonoBehaviour {
    private Vector3 m_rotation;
    public RectTransform m_rectTransform;

    // Use this for initialization
    void Start() {
        m_rotation = new Vector3(0f, 0f, 0f);
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(GetComponent<RectTransform>().rotation);
        GetComponent<RectTransform>().Rotate(
            m_rotation.x,
            m_rotation.y,
            m_rotation.z += 0.01f
        );
    }
}
