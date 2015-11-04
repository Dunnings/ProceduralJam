using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

    public SpriteRenderer m_image;
    public InventoryItem m_invItem;
    public AudioClip drop;
    public bool isPlaced = true;
    
    void Start()
    {

        //m_invItem = GameObject.Find(gameObject.name).GetComponent<InventoryItem>();
        //for (int i = 0; i < allinvItems.Length; i++)
        //{
        //    if(allinvItems[i].gameObject.name == gameObject.name) {
        //        m_invItem = allinvItems[i];
        //        return;
        //    }
        //}
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
        CharMovement.Instance.anim.Play("player_poke");
        AudioManager.GetInstance().PlaySingle(drop);
        Debug.Log("drop");
    }
}
