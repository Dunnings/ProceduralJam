using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class InventoryItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

    public InventorySlot InventorySlot;
    public Item m_item;

    private float timeMousePressed = 0f;

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Spawn()
    {
        gameObject.SetActive(true);
        InventoryManager.Instance.PlaceItemInSlot(this, InventoryManager.Instance.NextAvailableSlot());
    }

    public void Place()
    {
        //InventoryManager.Instance.RemoveItem(InventorySlot);
        m_item.Place(GameObject.FindGameObjectWithTag("Player").transform.position);
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CharMovement.Instance.isMouseOverUI = true;
        InventoryManager.Instance.OnInventoryItemPressed(this);
        timeMousePressed = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CharMovement.Instance.isMouseOverUI = false;
        if (Time.time - timeMousePressed < 0.3f)
        {
            InventoryManager.Instance.OnInventoryItemClick(this);
        }
        else
        {
            InventoryManager.Instance.OnInventoryItemReleased(this);
        }
    }
}
