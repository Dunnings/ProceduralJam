using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour {

    int day = 0;

    public Text m_dayCount;

    public static QuestManager Instance;

    public GameObject m_itemParent;
    public OxygenBar m_oxy;

    public Text m_questText;

    public List<string> m_requiredItems = new List<string>();
    
	void Awake () {
        Instance = this;
	}
	
	void Start () {
        GenerateNewQuest();
    }

    public bool IsThisItemRequired(GameObject item)
    {
        for (int i = 0; i < m_requiredItems.Count; i++)
        {
            if (m_requiredItems[i] == item.name)
            {
                return true;
            }
        }
        return false;
    }

    public void GenerateNewQuest()
    {
        m_requiredItems.Clear();
        List<Item> potentialItems = new List<Item>();
        for (int i = 0; i < m_itemParent.transform.childCount; i++)
        {
            if (m_itemParent.transform.GetChild(i).GetComponent<Item>().isPlaced) {
                potentialItems.Add(m_itemParent.transform.GetChild(i).GetComponent<Item>());
            }
        }
        if (potentialItems.Count > 0)
        {
            m_requiredItems.Add(potentialItems[Random.Range(0, potentialItems.Count)].gameObject.name);
            m_questText.text = "- Collect an " + m_requiredItems[0];
            TimeManager.Instance.m_time = 6f;
            m_oxy.m_decreaseOxygen = true;
            m_oxy.m_oxygenPercent = 1f;
            day++;
            m_dayCount.text = "Day " + day;
        }
        else
        {
            //SUCCESS YOU WIN
        }
    }

    public void FailedQuest()
    {
        //GAME OVER
    }
}
