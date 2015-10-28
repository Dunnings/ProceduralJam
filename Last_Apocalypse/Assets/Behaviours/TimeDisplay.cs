using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class TimeDisplay : MonoBehaviour {

    public Text m_text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_text.text = DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");
	}
}
