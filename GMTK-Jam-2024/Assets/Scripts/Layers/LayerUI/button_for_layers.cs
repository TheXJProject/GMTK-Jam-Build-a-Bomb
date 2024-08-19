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
    public bool LayerTaskWentWrong = false;

    private void Update()
    {
        if (task_setup_and_status.anyIsFocused) { gameObject.GetComponent<EventTrigger>().enabled = false; }
        else { gameObject.GetComponent<EventTrigger>().enabled = true; }

        if (LayerTaskWentWrong) { gameObject.GetComponent<Image>().color = Color.red; }
        else { gameObject.GetComponent<Image>().color = Color.white; }
    }

    public void LayerButtonPressed()
    {
        onLayerSelected?.Invoke(representedLayer);
    }

    
}
