﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Audio;

public class Intro : MonoBehaviour {

    public Text m_selector;

    void Start () {
    }

    void Update() {
        // Did in 2 seconds, pretty trash.
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Escape))
        {
            if (m_selector.transform.localPosition.y == -44) {
                Application.LoadLevel("Main");
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            m_selector.transform.localPosition = new Vector3(-46.23f, -104.5f, 0f);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            m_selector.transform.localPosition = new Vector3(-46.23f, -44f, 0f);
        }
	}
}
