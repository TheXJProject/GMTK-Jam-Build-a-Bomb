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

    private float currentTime;

    private int minutes;
    private int seconds;
    private string tempMinStr;
    private string tempSecStr;
    private string timeStr;

    private void OnEnable()
    {
        layer_controller.onTimerStart += StartTimer;
    }

    private void OnDisable()
    {
        layer_controller.onTimerStart -= StartTimer;
    }

    private void Update()
    {
        //curren

        //image.color = Color.red;

        timerTimeReadOut.text = FormText(currentTime);

    }

    private string FormText(float time)
    {
        minutes = (int)Math.Floor(time / 60f);
        seconds = (int)Math.Ceiling(time % 60);
        if (minutes < 10)
        {
            tempMinStr = "0" + minutes.ToString();
        }
        else
        {
            tempMinStr = minutes.ToString();
        }
        if (minutes < 10)
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
        PlayerTracking.Tracker.bestTime = currentTime;
    }
}
