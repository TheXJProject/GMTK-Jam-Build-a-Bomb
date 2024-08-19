using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private void OnEnable()
    {
        layer_controller.onTimerStart += StartTimer;
    }

    private void OnDisable()
    {
        layer_controller.onTimerStart -= StartTimer;
    }

    private void StartTimer()
    {

    }
}
