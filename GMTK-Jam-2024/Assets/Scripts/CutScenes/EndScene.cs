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
        Fade.onOneAlpha += slideInView;
    }
    private void OnDisable()
    {
        Fade.onZeroAlpha -= slideNotInView;
        Fade.onOneAlpha -= slideInView;
    }

    void Start()
    {
        if (true /*win*/)
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

        fade.ShowUI();
    }

    private void slideInView()
    {
        changing = false;
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
}
