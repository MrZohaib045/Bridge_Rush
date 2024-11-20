
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    
    AudioSource Bg;
    [SerializeField]
    AudioClip bg;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        gameObject.AddComponent<AudioSource>();
        Bg = GetComponent<AudioSource>();
        Bg.loop = true;
        Bg.clip = bg;
    }

    void Start()
    {
        
        Play();
    }

    public void Play()
    {
        if (PlayerPrefs.GetInt("music") == 0)
            Bg.Play();
    }
    public void Pause()
    {
        Bg.Pause();
    }
    
}
