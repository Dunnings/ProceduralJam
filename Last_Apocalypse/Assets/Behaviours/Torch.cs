using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour {

    public Light m_light;
    bool go = true;
    public float intensity = 2f;

	// Use this for initialization
	void Start () {
        Flicker();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0f, 0f, 90f * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0f, 0f, -90f * Time.deltaTime));
        }
    }

    public void Flicker()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", m_light.intensity, "to", Random.Range(intensity - 0.3f, intensity + 0.3f), "onupdate", "SetIntensity", "time", 0.1f, "easetype", iTween.EaseType.easeInOutCirc, "oncomplete", "Flicker"));
    }

    public void SetIntensity(float f)
    {
        m_light.intensity = f;
    }

}
