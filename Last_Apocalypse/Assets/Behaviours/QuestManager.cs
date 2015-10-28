using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {

    public static QuestManager Instance;

    public List<string> m_requiredItems = new List<string>();

	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
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
}
