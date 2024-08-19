using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracking : MonoBehaviour
{
    public static PlayerTracking Tracker;

    public enum winType
    {
        noWin,
        Win,
        Loss
    }

    public bool hardMode = false;
    public winType currentWinType = winType.noWin;
    public float bestTime = 1000f; //Change to Maximum time in seconds


    private void Awake()
    {
        if (Tracker == null)
        {
            Tracker = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("destroyed");
            Destroy(gameObject);
        }
    }
}
