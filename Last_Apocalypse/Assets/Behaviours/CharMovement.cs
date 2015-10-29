using UnityEngine;
using System.Collections;

public class CharMovement : MonoBehaviour {

    public float speed = 50.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            //transform.Rotate(mousePos * Time.deltaTime);
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position,
                                             mousePos,
                                             1 / (speed * (Vector3.Distance(gameObject.transform.position, mousePos))));
        }
	}
}
