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

    [Range(0.05f, 5f)]
    [SerializeField] private float fadeSpeed;

    public void ShowUI()
    {
        fadeIn = true;
    }
    public void HideUI()
    {
        fadeOut = true;
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime * fadeSpeed;
                if (myUIGroup.alpha >= 1)
                {
                    onOneAlpha?.Invoke();
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (myUIGroup.alpha > 0)
            {
                myUIGroup.alpha -= Time.deltaTime * fadeSpeed;
                if (myUIGroup.alpha <= 0)
                {
                    onZeroAlpha?.Invoke();
                    fadeOut = false;
                }
            }
        }
    }
}
