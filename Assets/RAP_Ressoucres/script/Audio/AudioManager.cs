using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Sound[] sounds;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

       // DontDestroyOnLoad(gameObject);

        foreach ( Sound s in sounds)
        {
        s.source =  gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }
    // Update is called once per frame
    private void Start()
    {
        
    }
    public void Play (string name)
    {
     Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if (s == null)
           return;
        s.source.Play();

    }
}
