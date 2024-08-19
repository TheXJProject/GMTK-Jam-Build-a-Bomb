using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button_for_layers : MonoBehaviour
{
    public static event Action<int> onLayerSelected;

    public int representedLayer;

    private void Update()
    {
        if (task_setup_and_status.anyIsFocused) { gameObject.GetComponent<EventTrigger>().enabled = false; }
        else { gameObject.GetComponent<EventTrigger>().enabled = true; }
    }

    public void LayerButtonPressed()
    {
        onLayerSelected?.Invoke(representedLayer);
    }

    
}
