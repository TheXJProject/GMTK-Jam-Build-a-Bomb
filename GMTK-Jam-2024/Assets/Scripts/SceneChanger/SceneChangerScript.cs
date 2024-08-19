using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerScript : MonoBehaviour
{
    private bool isLeaving = false;

    private void OnEnable()
    {
        += 
    }

    private void OnDisable()
    {
        
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
        if (PlayerTracking.Tracker.currentWinType == PlayerTracking.winType.noWin)
        {
            isLeaving = true;
            AudioManager.Instance.FadeOutMusic();
            PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Loss;

            // prepare to change to main menu
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
