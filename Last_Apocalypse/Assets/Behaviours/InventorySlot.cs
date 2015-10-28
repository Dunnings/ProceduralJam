using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventorySlot : MonoBehaviour {

    public Image m_image;
    public Color m_unHighlightedColor;
    public Color m_highlightedColor;

    public InventoryItem m_currentHeldItem;

    // Use this for initialization
    void Start () {
        m_image = GetComponent<Image>();
        UnHighlight();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Highlight()
    {
        m_image.color = m_highlightedColor;
    }

    public void UnHighlight()
    {
        m_image.color = m_unHighlightedColor;
    }
}
