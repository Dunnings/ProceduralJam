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

	}

    public void Flicker()
    {
        iTween.ValueTo(gameObject, iTween.Hash("from", m_light.intensity, "to", Random.Range(intensity - 0.3f, intensity + 0.3f), "onupdate", "SetIntensity", "time", 0.1f, "easetype", iTween.EaseType.easeInOutCirc, "oncomplete", "Flicker"));
    }

    public void SetIntensity(float f)
    {
        if (TimeManager.Instance.GetTime() > 18f || TimeManager.Instance.GetTime() < 6f)
        {
            m_light.intensity = f;
        }
        else
        {
            m_light.intensity = 0f;
        }
    }

}
