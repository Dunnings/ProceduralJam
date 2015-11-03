using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

    public AudioSource efxSource;
    public AudioSource musicSource;
    private static AudioManager instance;
    public static AudioManager GetInstance()
    {
        if(instance == null)
        {
            instance = new AudioManager();
        }
        return instance;
    }

    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    // Use this for initialization
    void Awake()
    {
        //if (instance == null)
        //    instance = this;
        //else if (instance != this)
        //    Destroy(gameObject);

        instance = this;

        DontDestroyOnLoad(gameObject);

    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }
    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }
}
