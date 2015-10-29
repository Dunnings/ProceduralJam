using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {
    public static TimeManager Instance;

    public float m_dayDurationInSeconds = 300;
    public float m_nightDurationInSeconds = 10;


    public Light m_light;

    public Image m_Image;
    public Text m_Text;
    public Color m_colMidnight;
    public Color m_colMidday;
    public float m_time = 6f;

    public GameObject m_sun;
    public GameObject m_moon;

    public GameObject m_leftGO;
    public GameObject m_topGO;
    public GameObject m_rightGO;

    private float m_left;
    private float m_top;
    private float m_bottom;
    private float m_right;

    private bool isNight = false;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        m_right = (transform.position - m_leftGO.transform.position).x;
        m_top = -(transform.position - m_topGO.transform.position).y;
        m_left = (transform.position -  m_rightGO.transform.position).x;
        m_bottom = -(transform.position - m_leftGO.transform.position).y;
    }
	
	// Update is called once per frame
	void Update () {
        if (isNight)
        {
            //12 hours in a day
            //1f/1f = an hour each second
            //1f/60f = an hour a minute
            //1f/360f = an hour an hour
            //12f/300f
           
            m_time += Time.deltaTime * (12f/(m_nightDurationInSeconds));
        }
        else
        {
            m_time += Time.deltaTime * (12f / (m_dayDurationInSeconds));
        }

        if (m_time > 24f) { m_time -= 24f; }
        if (m_time >= 6f && m_time < 18f)
        {
            if (isNight)
            {
                //JUST TURNED DAY
                m_moon.SetActive(false);
                m_sun.SetActive(true);
            }
            isNight = false;
            //DAY
            float dayPercent = (m_time - 6f) / 12f;
            float x = Mathf.Lerp(m_left, m_right, dayPercent);
            //float y = Mathf.Lerp(m_bottom, m_top, dayPercent);
            //if (dayPercent > 0.5f)
            //{
            //    y = Mathf.Lerp(m_top, m_bottom, dayPercent);
            //}
            float y = Mathf.Sin(dayPercent * Mathf.PI) * m_right - m_left;
            y *= 0.07f;
            y = m_bottom + y;
            m_sun.transform.position = transform.position + new Vector3(x, y, 0f);
        }
        else
        {
            if (!isNight)
            {
                //JUST TURNED NIGHT
                m_sun.SetActive(false);
                m_moon.SetActive(true);
            }
            isNight = true;
            //NIGHT
            float tempTime = m_time;
            if(m_time >= 18f)
            {
                tempTime = (m_time - 12f);
            }
            else
            {
                tempTime = 12f + m_time;
            }

            float nightPercent = (tempTime - 6f) / 12f;
            float x = Mathf.Lerp(m_left, m_right, nightPercent);
            //float y = Mathf.Lerp(m_bottom, m_top, nightPercent);
            //if (nightPercent > 0.5f)
            //{
            //    y = Mathf.Lerp(m_top, m_bottom, nightPercent);
            //}

            float y = Mathf.Sin(nightPercent * Mathf.PI) * m_right - m_left;
            y *= 0.07f;
            y = m_bottom + y;
            m_moon.transform.position = transform.position + new Vector3(x, y, 0f);
            
        }

        float colorVal = m_time;
        if(colorVal > 12f)
        {
            colorVal -= 12f;
            colorVal = 12f - colorVal;
        }

        m_Image.color = Color.Lerp(m_colMidnight, m_colMidday, colorVal / 12f);
        m_light.color = Color.Lerp(m_colMidnight, m_colMidday, colorVal / 12f);

        m_Text.text = (m_time - (m_time % 1f)).ToString("00") + ":" + ((m_time % 1f) * 59f).ToString("00");
	}

    public float GetTime() {
        return m_time;
    }

}
