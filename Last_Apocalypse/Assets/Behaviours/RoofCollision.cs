using UnityEngine;
using System.Collections;

public class RoofCollision : MonoBehaviour {

	public GameObject ParentBuilding;
	public int roofDesignation;
	
	void Start () {
	
	}
	

	void Update ()
	{	
	}

	void OnTriggerEnter(Collider other)
	{
		ParentBuilding.GetComponent<buildingRoof> ().collisions [roofDesignation] = true;
	}

	void OnTriggerExit(Collider other)
	{
		ParentBuilding.GetComponent<buildingRoof> ().collisions [roofDesignation] = false;
	}
}
