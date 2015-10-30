using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public static UIManager Instance;
    public Image m_mainBorder;
    public Image m_bottomBorder;
    public GameObject m_bottomBar;
    public GameObject m_bottomBarHidden;
    private Vector3 m_bottomBarPos;
    private Vector3 m_bottomBarHiddenPos;
    public bool bottomBarShown = true;
    

    public Color m_defaultColor;
    
    void Awake () {
        Instance = this;
	}
	
	void Start () {
        m_defaultColor = m_mainBorder.color;
        m_bottomBarPos = m_bottomBar.transform.position;
        m_bottomBarHiddenPos = m_bottomBarHidden.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
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

    public void ShowBottomBar()
    {
        iTween.MoveTo(m_bottomBar, iTween.Hash("y", m_bottomBarPos.y, "islocal", false, "time", 0.6f, "easetype", iTween.EaseType.easeOutBounce, "oncompletetarget", gameObject, "oncomplete", "ToggleBottomBarBool"));
    }
    public void HideBottomBar()
    {
        iTween.MoveTo(m_bottomBar, iTween.Hash("y", m_bottomBarHiddenPos.y, "islocal", false, "time", 0.6f, "easetype", iTween.EaseType.easeOutSine, "oncompletetarget", gameObject, "oncomplete", "ToggleBottomBarBool"));
    }

    public void ToggleBottomBarBool()
    {
        bottomBarShown = !bottomBarShown;
    }

    public void ToggleBottomBar()
    {
        if (bottomBarShown)
        {
            HideBottomBar();
        }
        else
        {
            ShowBottomBar();
        }
    }
}
