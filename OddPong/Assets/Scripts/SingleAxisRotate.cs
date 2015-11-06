using UnityEngine;

public class SingleAxisRotate : MonoBehaviour {
    public bool isNegative = false; // True if -n, false if +n.
    public float m_speed;
    public char m_axisType; // x y or z obv

    void Start() {
        if (m_axisType != 'x' && m_axisType != 'y' && m_axisType != 'z')
        {
            Debug.LogError("Unsupported or undefined axis type used.");
        }
    }
	
	// Update is called once per frame
	void Update () {
        var rotation = (Time.deltaTime * (isNegative ? -1 : 1)) * m_speed;
        GetComponent<RectTransform>().Rotate(
            m_axisType == 'x' ? rotation : 0,
            m_axisType == 'y' ? rotation : 0,
            m_axisType == 'z' ? rotation : 0
        );
    }
}
