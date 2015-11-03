using UnityEngine;
using System.Collections;

public class buildingRoof : MonoBehaviour {

	public GameObject[] RoofQuads;
	public bool[] collisions;

	bool rendering = true;

	void Start()
	{
		collisions = new bool[RoofQuads.Length];
	}

	void Update()
	{
		bool colliding = false;

		for (int i = 0; i < (RoofQuads.Length); i++)
		{
			if (collisions[i])
			{
				colliding = true;
				break;
			}
		}

		if (colliding && rendering)
		{
			for (int i = 0; i < (RoofQuads.Length); i++)
			{
				RoofQuads[i].GetComponent<MeshRenderer> ().enabled = false;
			}
			rendering = false;
		} 
		else if (!colliding && !rendering)
		{
			for (int i = 0; i < (RoofQuads.Length); i++)
			{
				RoofQuads[i].GetComponent<MeshRenderer> ().enabled = true;
			}
			rendering = true;
		}

	}
}
