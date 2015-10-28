using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeBackground : MonoBehaviour {
    public Image m_Image;
    public Text m_Text;
    public Color m_colMidnight;
    public Color m_colMidday;
    public float m_time = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        m_time += Time.deltaTime * 0.3f;
        if(m_time > 2f) { m_time -= 2f; }
        float val = m_time;
        if(val > 1f)
        {
            val -= 1f;
            val = 1f - val;
        }
        m_Image.color = Color.Lerp(m_colMidnight, m_colMidday, val);
        //Color c = (new Color(1f, 1f, 1f, 1f)) - m_Image.color;
        //c.a = 1f;
        //m_Text.color = c;
	}
}
