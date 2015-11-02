using UnityEngine;
using System.Collections;

public class QUESTTRIGGER : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter() {

        for (int i = 0; i < InventoryManager.Instance.m_inventorySlots.Count; i++)
        {
            if (InventoryManager.Instance.m_inventorySlots[i].m_image.color == InventoryManager.Instance.m_inventorySlots[i].m_highlightedColor)
            {
                InventoryManager.Instance.m_inventorySlots[i].m_currentHeldItem.gameObject.SetActive(false);
                InventoryManager.Instance.RemoveItem(InventoryManager.Instance.m_inventorySlots[i]);
                QuestManager.Instance.GenerateNewQuest();
            }
        }
    }

}
