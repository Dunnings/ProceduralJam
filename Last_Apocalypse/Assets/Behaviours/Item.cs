using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

    public SpriteRenderer m_image;
    public InventoryItem m_invItem;
    public bool isPlaced = true;
    
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
        CharMovement.Instance.anim.Play("player_poke");
    }
}
