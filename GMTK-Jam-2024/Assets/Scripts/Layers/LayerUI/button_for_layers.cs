using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class button_for_layers : MonoBehaviour
{
    public static event Action<int> onLayerSelected;
    public int representedLayer;

    Color temp;

    public bool LayerTaskWentWrong = false;
    public bool mouseHovering = false;

    private void Update()
    {
        if (LayerTaskWentWrong) { gameObject.GetComponent<Image>().color = Color.red; }
        else { gameObject.GetComponent<Image>().color = Color.white; }

        // MAKE SURE THIS GOES LAST!!!!!!!!!!!!
        if (mouseHovering && !task_setup_and_status.anyIsFocused)
        {
            gameObject.GetComponent<Image>().color *= 0.8f;
            temp = gameObject.GetComponent<Image>().color;
            temp.a = 1;
            gameObject.GetComponent<Image>().color = temp;
        }
    }

    public void LayerButtonPressed()
    {
        AudioManager.Instance.PlaySFX("Level Select");
        if (!task_setup_and_status.anyIsFocused)
        {
            onLayerSelected?.Invoke(representedLayer);
        }
    }

    public void LayerButtonStartedHovering()
    {
        mouseHovering = true;
    }

    public void LayerButtonStoppedHovering()
    {
        mouseHovering = false;
    }
}
