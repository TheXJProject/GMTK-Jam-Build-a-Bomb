using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    [SerializeField] Fade fade;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject loseImage;

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

        if (PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.Win)
        {
            winImage.SetActive(true);
            AudioManager.Instance.ToggleMusicLoop(false);
            AudioManager.Instance.PlayMusic("Winning Chord");
        }
        else if (PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.Loss)
        {
            loseImage.SetActive(true);
            AudioManager.Instance.PlayMusic("Fail Theme");
        }
        else
        {
            Debug.LogWarning("You didn't even play the game, you cheeky little bugger!");
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
            SceneManager.LoadScene("MainGameLoop"); // Will automatically go into the previous gamemode (hard/normal)
        }
        else
        {
            AudioManager.Instance.ToggleMusicLoop(true);
            SceneManager.LoadScene("StartMenu"); // Player can choose new gamemode
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
