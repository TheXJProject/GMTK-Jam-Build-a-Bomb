using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Fade : MonoBehaviour
{
    public static event Action onZeroAlpha;
    public static event Action onOneAlpha;
    
    [SerializeField] private CanvasGroup myUIGroup;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    [Range(0f, 5f)]
    [SerializeField] private double fadeSpeed;

    [Range(0f, 0.01f)]
    [SerializeField] private float acceleration;


    private float fadeAcceleration;

    public void ShowUI()
    {
        fadeIn = true;
        fadeAcceleration = acceleration / 100000;
        Debug.Log("Showing UI.");
    }
    public void HideUI()
    {
        fadeOut = true;
        fadeAcceleration = acceleration / 100000;
        Debug.Log("Hiding UI.");
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += (float)((Time.deltaTime + fadeAcceleration) * fadeSpeed);
                if (myUIGroup.alpha >= 1)
                {
                    onOneAlpha?.Invoke();
                    fadeIn = false;
                }
                fadeAcceleration /= 0.99f;
            }
        }

        if (fadeOut)
        {
            if (myUIGroup.alpha > 0)
            {
                myUIGroup.alpha -= (float)((Time.deltaTime + fadeAcceleration) * fadeSpeed);
                if (myUIGroup.alpha <= 0)
                {
                    onZeroAlpha?.Invoke();
                    fadeOut = false;
                }
                fadeAcceleration /= 0.99f;
            }
        }
    }
}
