using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

// Created by David Dunnings

public class HeatIndicator : MonoBehaviour {

    public Image m_Image;

    public Color m_hot;
    public Color m_cold;

    private float m_maxDistance = 50f;
    private float m_normalizedDistance = 0f;

    float lastFlashTime = 0f;
    
	void Awake(){
		
	}

	void Start () {
		
	}

    void Update() {
        if (!QuestManager.Instance.m_requiredItems[0].GetComponent<Item>().isPlaced)
        {
            m_Image.color = new Color(0f, 0f, 0f, 0f);
        }
        else { 
            m_normalizedDistance = Vector3.Distance(QuestManager.Instance.m_requiredItems[0].transform.position, CharMovement.Instance.transform.position) / m_maxDistance;
            m_Image.color = Color.Lerp(m_hot, m_cold, m_normalizedDistance);

            if (Time.time - lastFlashTime > m_normalizedDistance * 2f)
            {
                m_Image.color = new Color(0f, 0f, 0f, 0f);
                if (Time.time - lastFlashTime > (m_normalizedDistance * 2f) + 0.1f)
                {
                    lastFlashTime = Time.time;
                }
            }
        }
	}
	
	void OnDestroy(){
		
	}
}
