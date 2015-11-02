using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharItemAcquisition : MonoBehaviour {

    public Animator m_anim;
    public KeyCode m_pickUpKey = KeyCode.Space;
    public float m_pickupDistance = 0.2f;

    List<Item> m_listOfObjects = new List<Item>();

	public GameObject m_objectParent;

	// Use this for initialization
	void Start ()
	{
		for (int i = 0; i < m_objectParent.transform.childCount; i++)
		{
			m_listOfObjects.Add (m_objectParent.transform.GetChild(i).GetComponent<Item>());
		}
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(m_pickUpKey) && InventoryManager.Instance.SlotsAvailable() > 0)
        {
            Item closestObject = GetClosestItem();
            if(closestObject != null)
            {
                Debug.Log(Vector3.Distance(transform.position, closestObject.transform.position) < m_pickupDistance);
                if (Vector3.Distance(transform.position, closestObject.transform.position) < m_pickupDistance)
                {
                    closestObject.PickUp();
                    m_anim.Play("player_poke");
                }
            }
        }
	}

    public Item GetClosestItem()
    {
        float distance = float.MaxValue;
        Item closest = null;
        for (int i = 0; i < m_listOfObjects.Count; i++)
        {
            float tempDistance = Vector3.Distance(m_listOfObjects[i].transform.position, transform.position);
            if (tempDistance < distance)
            {
                distance = tempDistance;
                closest = m_listOfObjects[i];
            }
        }
        return closest;
    }
}
