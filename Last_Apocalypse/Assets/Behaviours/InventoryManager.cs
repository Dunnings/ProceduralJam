using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager Instance;

    public List<InventorySlot> m_inventorySlots = new List<InventorySlot>();
    InventoryItem heldItem;
    bool isHoldingInvItem = false;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (isHoldingInvItem)
        {
            heldItem.transform.position = Input.mousePosition;
        }
	}

    //Called when user presses down on an inventory item

    public void OnInventoryItemPressed(InventoryItem ii)
    {
        isHoldingInvItem = true;
        heldItem = ii;
        if (ii.InventorySlot != null) 
        {
            ii.InventorySlot.UnHighlight();
            ii.InventorySlot.m_currentHeldItem = null;
        }
    }

    public void OnInventoryItemReleased(InventoryItem ii)
    {
        isHoldingInvItem = false;
        InventorySlot closest = GetNearestInventorySlot(ii.transform.position);
        if (closest != null) {
            ii.transform.position = closest.transform.position;
            ii.InventorySlot = closest;
            ii.InventorySlot.m_currentHeldItem = ii;
            if (QuestManager.Instance.IsThisItemRequired(ii.gameObject)){
                closest.Highlight();
            }
        }
    }

    private InventorySlot GetNearestInventorySlot(Vector3 _pos)
    {
        float d = float.MaxValue;
        InventorySlot toReturn = null;
        for (int i = 0; i < m_inventorySlots.Count; i++)
        {
            if (m_inventorySlots[i].m_currentHeldItem == null)
            {
                float cD = Vector3.Distance(m_inventorySlots[i].transform.position, _pos);
                if (cD < d)
                {
                    d = cD;
                    toReturn = m_inventorySlots[i];
                }
            }
        }
        return toReturn;
    }
}
