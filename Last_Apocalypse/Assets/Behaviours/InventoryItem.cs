using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public InventorySlot InventorySlot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        InventoryManager.Instance.OnInventoryItemPressed(this);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InventoryManager.Instance.OnInventoryItemReleased(this);
    }
}
