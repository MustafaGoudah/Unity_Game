using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private static MusicController instance;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioSource source;
    public static MusicController Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        PlayGameMusic();
    }

    public void PlayGameMusic()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        source.clip = gameMusic;
        source.Play();
    }

    public void PlayMenuMusic()
    {
        if (source.isPlaying)
        {
           
            source.Stop();
        }
        source.PlayOneShot(menuMusic);
    }

    void Update()
    {
        
    }
}
