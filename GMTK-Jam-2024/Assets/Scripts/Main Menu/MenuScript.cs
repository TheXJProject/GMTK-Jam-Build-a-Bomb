using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private bool hardMode;
    private bool lockedIn = false;

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
        AudioManager.Instance.PlayMusic("Tense Song Intro");
        AudioManager.Instance.FadeInMusic();
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

}
