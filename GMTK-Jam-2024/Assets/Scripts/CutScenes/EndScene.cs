using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] Fade fade;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject loseImage;

    private bool changing = true;
    private bool playAgain;
    private bool lockedIn = false;

    private void OnEnable()
    {
        Fade.onZeroAlpha += slideNotInView;
    }
    private void OnDisable()
    {
        Fade.onZeroAlpha -= slideNotInView;
    }

    void Start()
    {
        fade.ShowUI();

        if (false /*win*/)
        {
            winImage.SetActive(true);
            AudioManager.Instance.ToggleMusicLoop(false);
            AudioManager.Instance.PlayMusic("Winning Chord");
        }
        else /*loss*/
        {
            loseImage.SetActive(true);
            AudioManager.Instance.PlayMusic("Fail Theme");
        }
        AudioManager.Instance.FadeInMusic();
    }

    public void PlayMode(bool again)
    {
        if (!lockedIn)
        {
            playAgain = again;
        }
        lockedIn = true;
    }

    private void slideNotInView()
    {
        if (playAgain)
        {
            AudioManager.Instance.ToggleMusicLoop(true);
            SceneManager.LoadScene("MainGameLoop");
        }
        else
        {
            AudioManager.Instance.ToggleMusicLoop(true);
            SceneManager.LoadScene("StartMenu");
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
