using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    public Image m_mainBorder;
    public Image m_bottomBorder;

    public Color m_defaultColor;
    
    void Awake () {
        Instance = this;
	}
	
	void Start () {
        m_defaultColor = m_mainBorder.color;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FlashDamage();
        }
    }

    public void UpdateBorderColor(Color c)
    {
        m_bottomBorder.color = c;
        m_mainBorder.color = c;
    }

    public void FlashDamage()
    {
        UpdateBorderColor(new Color(0.5f, 0.1f, 0.1f, 1f));
        StartCoroutine(ReturnNormalColor(0.1f));
    }

    IEnumerator ReturnNormalColor(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        UpdateBorderColor(m_defaultColor);
    }
}
