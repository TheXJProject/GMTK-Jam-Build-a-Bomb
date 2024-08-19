using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layer_controller : MonoBehaviour
{
    public static event Action<float> onNewLayerCreated;

    [SerializeField] float layerSizeIncrease = 3f;
    [SerializeField] float layerSizeIncAccelerate = 2f;
    [SerializeField] List<GameObject> uniqueLayers;
    [SerializeField] List<GameObject> layers;

    int thisLayer = 0;
    private void OnEnable()
    {
        layer_task_controller.onLayerSolved += createNewLayer;
        createCoreLayer();
    }

    private void OnDisable()
    {
        layer_task_controller.onLayerSolved -= createNewLayer;
    }

    void createCoreLayer()
    {
        layers.Add(Instantiate(uniqueLayers[thisLayer], Vector2.zero, Quaternion.identity, this.transform));
        onNewLayerCreated?.Invoke(layers[thisLayer].transform.localScale.x);
    }

    void createNewLayer(int prevLayer)
    {
        thisLayer = prevLayer + 1;
        layers.Add(Instantiate(uniqueLayers[thisLayer], Vector2.zero, Quaternion.identity, this.transform));
        layers[thisLayer].transform.localScale *= thisLayer * layerSizeIncrease;
        layerSizeIncrease *= layerSizeIncAccelerate;
        onNewLayerCreated?.Invoke(layers[thisLayer].transform.localScale.x);
    }
}
