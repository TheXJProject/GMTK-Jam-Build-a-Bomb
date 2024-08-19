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

    private void Update()
    {
        if ((PlayerTracking.Tracker.currentWinType != PlayerTracking.winType.noWin) && !isLeaving)
        {
            isLeaving = true;
            AudioManager.Instance.FadeOutMusic();
            // prepare to change to end scene
        }
    }

    public void MainMenu()
    {
        if ((PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.noWin) && !isLeaving)
        {
            isLeaving = true;
            AudioManager.Instance.FadeOutMusic();
            PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Loss;

            // prepare to change to main menu
        }
    }

    public void Victorrrrryyy()
    {
        if ((PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.noWin) && !isLeaving)
        {
            isLeaving = true;
            AudioManager.Instance.FadeOutMusic();
            PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Win;

            // prepare to change to End Scene
        }
    }

    private void ChangeToMainMenu ()
    {
        SceneManager.LoadScene("StartMenu");
    }

    private void ChangeToEndScene ()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
