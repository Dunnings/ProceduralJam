using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    public static GameCamera Instance;
    public GameObject m_target;
    public GameObject m_parent; 

	// Use this for initialization
	void Awake ()
    {
        Instance = this; 
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(m_target.transform.position.x, m_target.transform.position.y, transform.position.z), Time.deltaTime * (1f + (Vector3.Distance(m_target.transform.position, transform.position) * 2f)));
	}

    public void ShakeCamera()
    {
        iTween.ShakePosition(m_parent, iTween.Hash("x", 0.15f, "y", 0.15f, "time", 0.6f));
    }
}
