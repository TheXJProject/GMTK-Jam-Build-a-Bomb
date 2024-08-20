using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] TMP_Text timerTimeReadOut;

    [Range(10f,1000f)]
    [SerializeField] float startingTimeAmount = 300f;
    [Range(1f, 10f)]
    [SerializeField] float timerSpeedIncrease = 1f;

    private float currentTime;

    private bool timerStopped = false;

    private bool drainingTime = false;

    private int minutes;
    private int seconds;
    private string tempMinStr;
    private string tempSecStr;
    private string timeStr;

    private int failCounter = 0;

    private void OnEnable()
    {
        layer_controller.onTimerStart += StartTimer;
        layer_controller.onVictoryRoyale += StopTimer;

        task_activate_and_cancel.onTaskGoesWrong += FailCounterIncrease;
        task_setup_and_status.onWrongTaskCorrected += FailCounterDecrease;
    }

    private void OnDisable()
    {
        layer_controller.onTimerStart -= StartTimer;
        layer_controller.onVictoryRoyale -= StopTimer;

        task_activate_and_cancel.onTaskGoesWrong -= FailCounterIncrease;
        task_setup_and_status.onWrongTaskCorrected -= FailCounterDecrease;
    }

    private void Update()
    {
        if ((currentTime > 0) && !timerStopped)
        {
            if (failCounter > 0)
            {
                image.color = Color.red;
                currentTime -= Time.deltaTime * timerSpeedIncrease;
            }
            else
            {
                image.color = Color.white;
                currentTime -= Time.deltaTime;
            }

            PlayerTracking.Tracker.currentTime = currentTime;
            timerTimeReadOut.text = FormText(currentTime);
        }

        if (currentTime < 0)
        {
            timerStopped = true;
            currentTime = 0;
            PlayerTracking.Tracker.currentTime = currentTime;
            timerTimeReadOut.text = FormText(currentTime);

            PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Loss;

            Debug.Log("Player looses: Ran Out Of Time!");
        }
    }

    private string FormText(float time)
    {
        minutes = (int)(time/ 60f);
        seconds = (int)(Math.Floor(time % 60));
        if (minutes < 10)
        {
            tempMinStr = "0" + minutes.ToString();
        }
        else
        {
            tempMinStr = minutes.ToString();
        }
        if (seconds < 10)
        {
            tempSecStr = "0" + seconds.ToString();
        }
        else
        {
            tempSecStr = seconds.ToString();
        }
        timeStr = tempMinStr + ":" + tempSecStr;

        return timeStr;
    }

    private void StartTimer()
    {
        currentTime = startingTimeAmount;
        PlayerTracking.Tracker.currentTime = currentTime;
        timerTimeReadOut.text = FormText(currentTime);
    }

    private void StopTimer()
    {
        timerStopped = true;
        PlayerTracking.Tracker.currentTime = currentTime;
        timerTimeReadOut.text = FormText(currentTime);

        if ((startingTimeAmount - currentTime) < PlayerTracking.Tracker.bestTime)
        {
            PlayerTracking.Tracker.bestTime = startingTimeAmount - currentTime;
        }
    }

    private void FailCounterIncrease(int layerp)
    {
        failCounter++;
    }
    
    private void FailCounterDecrease(int layerp)
    {
        failCounter--;
    }
}
