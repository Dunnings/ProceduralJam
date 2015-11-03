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

    public void OnInventoryItemReleased(InventoryItem iI)
    {
        isHoldingInvItem = false;
        
        InventorySlot closest = GetNearestInventorySlot(iI.transform.position);
        if (closest != null)
        {
            iI.transform.position = closest.transform.position;
            iI.InventorySlot = closest;
            closest.m_currentHeldItem = iI;
            if (QuestManager.Instance.IsThisItemRequired(iI.gameObject))
            {
                closest.Highlight();
            }
        }
    }

    public void PlaceItemInSlot(InventoryItem iI, InventorySlot iS)
    {
        iI.transform.position = iS.transform.position;
        iI.InventorySlot = iS;
        iS.m_currentHeldItem = iI;
        if (QuestManager.Instance.IsThisItemRequired(iI.gameObject))
        {
            iS.Highlight();
            QuestManager.Instance.PickedUpObject();
        }
    }

    public void RemoveItem(InventorySlot iS)
    {
        if (iS != null)
        {
            iS.UnHighlight();
            iS.m_currentHeldItem = null;
        }
    }

    public void OnInventoryItemClick(InventoryItem iI)
    {
        isHoldingInvItem = false;
        iI.Place();
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

    public int SlotsAvailable()
    {
        int count = 0;
        for (int i = 0; i < m_inventorySlots.Count; i++)
        {
            if(m_inventorySlots[i].m_currentHeldItem != null) {
                count++;
            }
        }
        return m_inventorySlots.Count - count;
    }

    public InventorySlot NextAvailableSlot()
    {
        for (int i = 0; i < m_inventorySlots.Count; i++)
        {
            if (m_inventorySlots[i].m_currentHeldItem == null)
            {
                return m_inventorySlots[i];
            }
        }
        return null;
    }
}
