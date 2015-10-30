using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

    public SpriteRenderer m_image;
    public InventoryItem m_invItem;
    public bool isPlaced = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PickUp()
    {
        if (isPlaced)
        {
            gameObject.SetActive(false);
            if (m_invItem)
            {
                m_invItem.Spawn();
            }
            isPlaced = false;
        }
    }

    public void Place(Vector3 pos)
    {
        gameObject.SetActive(true);
        transform.position = pos;
        isPlaced = true;
    }
}
