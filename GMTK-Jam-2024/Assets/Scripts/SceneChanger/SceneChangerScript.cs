using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    private bool isLeaving = false;

    private void OnEnable()
    {
        layer_controller.onVictoryRoyale += Victorrrrryyy;
    }

    private void OnDisable()
    {
        layer_controller.onVictoryRoyale -= Victorrrrryyy;
    }

    private void Start()
    {
        if (PlayerTracking.Tracker.hardMode)
        {
            AudioManager.Instance.PlayMusic("Hardmode");
        }
        else
        {
            AudioManager.Instance.PlayMusic("Standard");
        }
    }

    private void Update()
    {
        if ((PlayerTracking.Tracker.currentWinType != PlayerTracking.winType.noWin) && !isLeaving)
        {
            isLeaving = true;
            
            
            //AudioManager.Instance.FadeOutMusic();
            // Alter to be smoother in future
            
            
            AudioManager.Instance.StopMusic();
            AudioManager.Instance.StopSFX();
            ChangeToEndScene();
        }
    }

    public void MainMenu()
    {
        if ((PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.noWin) && !isLeaving)
        {
            isLeaving = true;
            PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Loss;

            AudioManager.Instance.StopMusic();
            AudioManager.Instance.StopSFX();
            ChangeToMainMenu();
        }
    }

    public void Victorrrrryyy()
    {
        if ((PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.noWin) && !isLeaving)
        {
            isLeaving = true;
            PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Win;

            //AudioManager.Instance.FadeOutMusic();
            // Alter to be smoother in future

            AudioManager.Instance.StopMusic();
            AudioManager.Instance.StopSFX();
            ChangeToEndScene();
        }
    }

    private void ChangeToMainMenu ()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void ChangeToEndScene ()
    {
        SceneManager.LoadScene("EndScene");
    }
}
