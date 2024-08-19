using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private bool hardMode;
    private bool lockedIn = false;

    private bool playedSound = false;

    private void OnEnable()
    {
        Fade.onZeroAlpha += PlayGame;
    }
    private void OnDisable()
    {
        Fade.onZeroAlpha -= PlayGame;
    }

    private void Start()
    {
        if (!playedSound)
        {
            AudioManager.Instance.PlayMusic("Tense Song Intro");
            AudioManager.Instance.FadeInMusic();
            playedSound = true;
        }
    }

    public void PlayMode(bool hard)
    {
        if (!lockedIn)
        {
            hardMode = hard;
        }
        lockedIn = true;
    }

    private void PlayGame()
    {
        if (hardMode)
        {
            SceneManager.LoadScene("HardModeCutScene");
        }
        else
        {
            SceneManager.LoadScene("CutScene");
        }
    }

    public void ButtonPressSound()
    {
        AudioManager.Instance.PlaySFX("Button Key Press");
    }

    public void ButtonFadeOutMusic()
    {
        AudioManager.Instance.FadeOutMusic(); ;
    }

}
