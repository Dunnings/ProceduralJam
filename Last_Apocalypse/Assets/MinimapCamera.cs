using UnityEngine;
using System.Collections;

public class MinimapCamera : MonoBehaviour {

    public GameObject m_player;
    public Camera m_cam;

    void Start()
    {
    }

    int deferrer = 0;
    bool boop = true;

    public void Update()
    {
        if (boop)
        {
            m_cam.enabled = (false);
            boop = false;
        }
        if (deferrer == 200)
        {
            m_cam.enabled = (true);
        }
        else if (deferrer > 200)
        {
            m_cam.enabled = (false);
            deferrer = 0;
        }

        deferrer++;
        transform.position = new Vector3(m_player.transform.position.x, m_player.transform.position.y, transform.position.z);
    }
}
