﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour {

	GameObject[] WorldGrid;

	public GameObject Player, ItemParent;

	public GameObject[] Prefabs;


	public int WorldPanelXYCount, PanelXYCount;

	int worldSize;

	void Start ()
	{
		worldSize = WorldPanelXYCount * WorldPanelXYCount;

		WorldGrid = new GameObject[worldSize];

		Generate (splitPrefabs(Prefabs));
	}


	GameObject[][] splitPrefabs(GameObject[] _prefabs)
	{
		//make other lists and remove unitl only the base is left
		List<GameObject> tempBuildings = new List<GameObject>(), tempEnvironments = new List<GameObject>();
		GameObject tempBase = new GameObject("Base");

		for (int i = 0; i < _prefabs.Length; i++)
		{
			switch (_prefabs[i].GetComponent<Panel>().type)
			{
			case Panel.tileType.Base:
				tempBase = _prefabs[i];
				break;

			case Panel.tileType.Building:
				tempBuildings.Add(_prefabs[i]);
				break;

			case Panel.tileType.Environment:
				tempEnvironments.Add(_prefabs[i]);
				break;
			
			default:
				break;
			}
		}//the prefabs should now be filtered into BASE, BUILDING, and ENVIRONMENT

		GameObject[][] splitPrefs = new GameObject[3][];

		splitPrefs [0] = new GameObject[1];
		splitPrefs [0] [0] = tempBase;

		splitPrefs [1] = tempBuildings.ToArray ();
		splitPrefs [2] = tempEnvironments.ToArray ();

		return splitPrefs;
	}


	void Generate (GameObject[][] _prefabs)
	{
		int row, x = 0, lastY = 0, y = 0, buildingsToPlace = _prefabs [1].Length, buildings = buildingsToPlace;

		//checks if the building has been placed TRUE = PLACED, FALSE = NOT PLACED
		bool[] buildingsPlaced = new bool[_prefabs[1].Length];


		//place player base
		WorldGrid [(worldSize/2)] = _prefabs [0] [0];

		//place ALL buildings
		while (buildingsToPlace > 0)
		{
			for (int i = 0; i < worldSize; i++)
			{
				if (WorldGrid[i] == null)
				{
					//randomly place things (kind of equally)
					if (Random.Range(0, ((int)(worldSize/_prefabs[1].Length))) == 0)
					{
						int randombuilding = Random.Range (0, buildings); 

						//makes sure the random building has not been placed yet
						while (buildingsPlaced[randombuilding])
						{
							randombuilding = Random.Range (0, buildings);
						}

						//set that grid pos to the random building
						WorldGrid[i] = _prefabs[1][randombuilding];
						buildingsPlaced[randombuilding] = true;

						//decrement buildingsToPlace
						buildingsToPlace--;

						//check if buildingsToPlace has reached 0
						if (buildingsToPlace == 0)
						{
							break;
						}
					}
				}
			}
		}

		//now fill the gaps with random environment tiles
		for (int i = 0; i < worldSize; i++)
		{
			if (WorldGrid[i] == null)
			{
				WorldGrid[i] = _prefabs[2][Random.Range (0, _prefabs[2].Length)];
			}
		}
		
		
		for (int i = 0; i < worldSize; i++)
		{
			row = i / WorldPanelXYCount;

			if (row != lastY) {
				y++;
				x = 0;
			} else if (i != 0) {
				x++;
			}

			WorldGrid [i] = (GameObject)Instantiate (WorldGrid [i], new Vector3 (x * PanelXYCount, y * PanelXYCount, 0), Quaternion.identity);

			WorldGrid [i].transform.parent = transform;

			lastY = row;
		}

		int spawnLoc = (WorldPanelXYCount / 2) * PanelXYCount;

		Player.transform.position = new Vector3 (spawnLoc, spawnLoc-1, 0);


		//place items

		//this is the list of spawned objects that can be used by the questgiver system
		List<GameObject> spawnedObjects = new List<GameObject> ();

		//all of the item locations
		GameObject[] SpawnLocations = GameObject.FindGameObjectsWithTag ("Item");

		//copy children so selection is not altered by adding each object.
		GameObject[] children = new GameObject[ItemParent.transform.childCount];

		for (int i = 0; i < ItemParent.transform.childCount; i++)
		{
			children[i] = ItemParent.transform.GetChild(i).gameObject;
		}

		int s = 0;

		for (int i = 0; i < SpawnLocations.Length - 1; i++)
		{
			if (s >= SpawnLocations.Length - 1)
			{
				s = 0;
			}
			//select a random number from 1 to the size of children
			int itemToSpawn = s;

			//selects a random int that corresponds to a different item
			//GameObject newObject = ItemParent.transform.GetChild(itemToSpawn);

			//if the selected item has already been spawned
			if (spawnedObjects.Contains(ItemParent.transform.GetChild(itemToSpawn).gameObject))
			{
				GameObject newObject = ItemParent.transform.GetChild(itemToSpawn).gameObject;

				newObject.transform.position = SpawnLocations[i].transform.position;

				newObject.transform.parent = ItemParent.transform;
			}
			else
			{
				ItemParent.transform.GetChild(itemToSpawn).position = SpawnLocations[i].transform.position;
				spawnedObjects.Add(ItemParent.transform.GetChild(itemToSpawn).gameObject);
			}

			s++;
		}
	}
}
