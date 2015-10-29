using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BarToggle : MonoBehaviour {

    public Image m_image;
    public Sprite m_open;
    public Sprite m_close;
    bool animating = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleImage()
    {
        if (!animating)
        {
            StartCoroutine(Toggle());
            animating = true;
        }
    }

    IEnumerator Toggle()
    {
        yield return new WaitForSeconds(0.6f);
        if (m_image.sprite == m_open)
        {
            m_image.sprite = m_close;
        }
        else
        {
            m_image.sprite = m_open;
        }
        animating = false;
    }
}
