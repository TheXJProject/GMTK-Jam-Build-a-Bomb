using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    [SerializeField] AudioFade fader;

    [Range(0f,1f)]
    [SerializeField] float startingSFXVolume;
    
    [Range(0f,1f)]
    [SerializeField] float startingMusicVolume;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("destroyed");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SFXVolume(startingSFXVolume);
        MusicVolume(startingMusicVolume);
    }

    public void PlayMusic(string name, bool oneShot = false)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + s.name + " Not Found, Idiot!");
        }
        else
        {
            if (oneShot)
            {
                musicSource.PlayOneShot(s.clip);
            }
            else
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
            // Debug.Log("Sound: " + s.name + "played.");
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + s.name + " Not Found, Idiot!");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
            // Debug.Log("Sound: " + s.name + "played.");
        }
    }

    public void ToggleMusicLoop(bool loop)
    {
        musicSource.loop = loop;
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public float GetVolumeMusic()
    {
        return musicSource.volume;
    }
    
    public float GetVolumeSFX()
    {
        return musicSource.volume;
    }

    public void FadeInMusic()
    {
        fader.FadeInVolume();
    }

    public void FadeOutMusic()
    {
        fader.FadeOutVolume();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }
    
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }
}
