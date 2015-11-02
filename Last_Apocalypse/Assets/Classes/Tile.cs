using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour
{
	public bool collidable, item;

	//the material to be placed once the item is gone
	public Material itemTakenMaterial;

	void Start ()
	{
		if (collidable)
		{
			gameObject.AddComponent<BoxCollider2D>();
		}
	}
}
