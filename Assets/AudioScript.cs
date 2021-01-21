using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public bool VolumeWasChanged;
    public List<AudioClip> soundtrack;
    public  AudioSource song;
    public AudioClip test;
    protected float MusicVolume;
    void Start()
    {
        VolumeWasChanged = true;
        if (VolumeWasChanged)
        {
            VolumeWasChanged = false;
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.5f);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.05f);
            song.volume = MusicVolume;


        }
    
        PlayNextSong();

    }
    void Update()
    {
        if (VolumeWasChanged)
        {
            VolumeWasChanged = false;
            AudioListener.volume = PlayerPrefs.GetFloat("Volume", 0.5f);
            MusicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.05f);
            song.volume = MusicVolume;

        }
    }

    public void PlayNextSong()
    {
        song.clip = soundtrack[Random.Range(0, soundtrack.Count)];
        
        song.volume = MusicVolume;
        song.Play();
        Invoke("PlayNextSong", song.clip.length);
    }
}
