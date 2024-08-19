using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class button_for_layers : MonoBehaviour
{
    public static event Action<int> onLayerSelected;

    public int representedLayer;

    public void LayerButtonPressed()
    {
        onLayerSelected?.Invoke(representedLayer);
    }

    
}
