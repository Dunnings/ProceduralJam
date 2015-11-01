﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGen : MonoBehaviour {

	GameObject[] WorldGrid;

	public GameObject Player;

	public GameObject[] Prefabs, Items;


	void Start ()
	{
		WorldGrid = new GameObject[225];

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
		WorldGrid [128] = _prefabs [0] [0];

		//place ALL buildings
		while (buildingsToPlace > 0)
		{
			for (int i = 0; i < 225; i++)
			{
				if (WorldGrid[i] == null)
				{
					//randomly place things (kind of equally)
					if (Random.Range(0, ((int)(225/_prefabs[1].Length))) == 0)
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
		for (int i = 0; i < 225; i++)
		{
			if (WorldGrid[i] == null)
			{
				WorldGrid[i] = _prefabs[2][Random.Range (0, _prefabs[2].Length)];
			}
		}
		
		
		for (int i = 0; i < 225; i++)
		{
			row = i / 15;

			if (row != lastY) {
				y++;
				x = 0;
			} else if (i != 0) {
				x++;
			}

			WorldGrid [i] = (GameObject)Instantiate (WorldGrid [i], new Vector3 (x * 15, y * 15, 0), Quaternion.identity);

			WorldGrid [i].transform.parent = transform;

			lastY = row;
		}

		Player.transform.position = new Vector3 (120, 120, 0);


		//place items
		List<GameObject> spawnedObjects = new List<GameObject> ();


		GameObject[] SpawnLocations = GameObject.FindGameObjectsWithTag ("Item");

		for (int i = 0; i < SpawnLocations.Length - 1; i++)
		{
			int itemToSpawn = Random.Range(0, Items.Length - 1);

			//sets the next spawnlocation's sprite to be the sprite of the randomly selected item
			SpawnLocations[i].GetComponent<SpriteRenderer>().sprite = Items[itemToSpawn].GetComponent<SpriteRenderer>().sprite;
			SpawnLocations[i].GetComponent<PickupItem>().type = Items[itemToSpawn].GetComponent<PickupItem>().type;

			spawnedObjects.Add (Items[itemToSpawn]);
		}

		GameObject[] allSpawnedObjects = spawnedObjects.ToArray ();
	}
}
