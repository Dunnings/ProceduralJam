using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour {

	GameObject[] WorldGrid;

	public GameObject[] Prefabs;

	void Start ()
	{
		WorldGrid = new GameObject[225];
		Generate ();
	}


	GameObject[][] splitPrefabs(GameObject[] _prefabs)
	{
		//make other lists and remove unitl only the base is left
		List<GameObject> buildings = new List<GameObject>(), environment = new List<GameObject>();

		for (int i = 0; i < _prefabs.Length; i++)
		{

		}

		return null;
	}


	void Generate ()
	{
		int row, x = 0, lastY = 0, y = 0;

		//currently fills the world with the test tile
		for (int i = 0; i < 225; i++)
		{
			row = i/15;

			if (row != lastY)
			{
				y++;
				x = 0;
			}else if (i !=0)
			{
				x++;
			}

			WorldGrid[i] = (GameObject)Instantiate(Prefabs[0], new Vector3(x, y, 0), Quaternion.identity);

			WorldGrid[i].transform.parent = transform;

			lastY = row;
		}
	}
}
