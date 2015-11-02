using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start ()
	{
		float temp = transform.position.z;
		Vector3 temp3 = Player.transform.position;

		transform.position = new Vector3 (temp3.x, temp3.y, temp);
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{
		float temp = transform.position.z;
		Vector3 temp3 = Player.transform.position;
		
		transform.position = new Vector3 (temp3.x, temp3.y, temp);
	}
}
