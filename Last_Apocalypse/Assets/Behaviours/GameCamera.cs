using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public GameObject m_target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.Lerp(transform.position, new Vector3(m_target.transform.position.x, m_target.transform.position.y, transform.position.z), Time.deltaTime * (1f + (Vector3.Distance(m_target.transform.position, transform.position) * 2f)));
	}
}
