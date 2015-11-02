using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OxygenBar : MonoBehaviour {

    public float m_oxygenDecreasePerSecond = 0.05f;

    public float m_oxygenPercent = 1.0f;
    public bool m_decreaseOxygen = false;

    public Color m_colHigh;
    public Color m_colMed;
    public Color m_colLow;
    public Color m_colCritical;

    public Image m_image;
    public RectTransform m_oxyBar;
    private float m_startWidth;

	// Use this for initialization
	void Start () {
        m_startWidth = m_oxyBar.sizeDelta.x;
	}
	
	// Update is called once per frame
	void Update () {
        m_oxygenPercent = Mathf.Clamp(m_oxygenPercent, 0f, 1f);
        m_oxyBar.sizeDelta = new Vector2(m_startWidth * m_oxygenPercent, m_oxyBar.sizeDelta.y);
        if (m_oxygenPercent > 0.7f)
        {
            float lerpVal = (m_oxygenPercent - 0.7f) / 0.3f;
            m_image.color = Color.Lerp(m_colMed, m_colHigh, lerpVal);
        }
        else if (m_oxygenPercent > 0.4f)
        {
            float lerpVal = (m_oxygenPercent - 0.4f) / 0.3f;
            m_image.color = Color.Lerp(m_colLow, m_colMed, lerpVal);
        }
        else
        {
            float lerpVal = m_oxygenPercent / 0.4f;
            m_image.color = Color.Lerp(m_colCritical, m_colLow, lerpVal);
        }

        if (m_decreaseOxygen)
        {
            m_oxygenPercent -= Time.deltaTime * m_oxygenDecreasePerSecond;
        }
        if(m_oxygenPercent <= 0f)
        {
            Application.LoadLevel("_GameOver");
        }
    }
}
