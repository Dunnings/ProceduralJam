using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TitleText : MonoBehaviour {
    
    public List<GameObject> m_objectsToFlicker = new List<GameObject>();
    bool quit = false;

	// Use this for initialization
	void Start ()
    {
        FadeToRandom();
    }

    void OnDestroy()
    {
        quit = true;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void FadeToRandom()
    {
        if (!quit)
        {
            float rand = Random.Range(0.2f, 0.4f);
            float alphaRand = Random.Range(0.15f, 0.2f);
            for (int i = 0; i < m_objectsToFlicker.Count; i++)
            {
                iTween.FadeTo(m_objectsToFlicker[i], iTween.Hash("time", rand, "alpha", alphaRand, "easetype", iTween.EaseType.easeInOutQuad));
            }
            StartCoroutine(CallFadeAfterDelay(rand));
        }
    }

   IEnumerator CallFadeAfterDelay(float _f)
    {
        yield return new WaitForSeconds(_f);
        FadeToRandom();
    }
}
