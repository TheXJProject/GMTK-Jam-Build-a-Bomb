using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioFade : MonoBehaviour
{
    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    [Range(0f, 5f)]
    [SerializeField] private double fadeSpeed;

    private float fadeVolume;
    private float temptVolume;

    private void Start()
    {
        temptVolume = AudioManager.Instance.GetVolumeMusic();
        fadeVolume = 1f;
    }

    public void FadeInVolume()
    {
        if (!fadeIn && !fadeOut && (fadeVolume == 0)) 
        {
            // Debug.Log("Fade In Music!");
            fadeIn = true;
        }
    }
    public void FadeOutVolume()
    {
        if (!fadeIn && !fadeOut && (fadeVolume == 1))
        {
            // Debug.Log("Fade Out Music!");
            fadeOut = true;
            temptVolume = AudioManager.Instance.GetVolumeMusic();
        }
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (fadeVolume < 1)
            {
                fadeVolume += (float)((Time.deltaTime) * fadeSpeed);
                AudioManager.Instance.MusicVolume(fadeVolume * temptVolume);
                
                if (fadeVolume >= 1)
                {
                    fadeVolume = 1f;
                    fadeIn = false;
                    // Debug.Log("Ready to fade out music.");
                }
            }
        }

        if (fadeOut)
        {
            if (fadeVolume > 0)
            {
                fadeVolume -= (float)((Time.deltaTime) * fadeSpeed);
                AudioManager.Instance.MusicVolume(fadeVolume * temptVolume);
                
                if (fadeVolume <= 0)
                {
                    fadeVolume = 0f;
                    fadeOut = false;
                    // Debug.Log("Ready to fade in music.");
                }
            }
        }
    }
}
