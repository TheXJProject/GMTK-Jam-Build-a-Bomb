using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class layer_controller : MonoBehaviour
{
    public static event Action onTimerStart;
    public static event Action onThingsStartGoingWrong;
    public static event Action onVictoryRoyale;
    public static event Action onTaskCompleteForButtons;
    public static event Action<int> onLayerSolved;
    public static event Action<float> onNewLayerCreated;

    public List<GameObject> layers;
    [SerializeField] List<GameObject> uniqueLayers;
    [SerializeField] float layerSizeIncrease = 3f;
    [SerializeField] float layerSizeIncAccelerate = 2f;

    int thisLayer = 0;
    private void OnEnable()
    {
        onLayerSolved += createNewLayer;
        task_setup_and_status.onTaskComplete += CheckFinishedAllLayers;
        camera_controller.onCameraReady += createCoreLayer;
    }

    private void OnDisable()
    {
        onLayerSolved -= createNewLayer;
        task_setup_and_status.onTaskComplete -= CheckFinishedAllLayers;
        camera_controller.onCameraReady -= createCoreLayer;
    }

    void CheckFinishedAllLayers(int taskLayer)
    {
        for (int i = 0; i < layers.Count; i++)
        {
            if (!layers[i].GetComponent<layer_task_controller>().CheckFinishedThisLayer()) { return; }
        }
        if (taskLayer == 9) 
        {
            onVictoryRoyale?.Invoke();
            return;
        }
        onLayerSolved?.Invoke(thisLayer);
        onTaskCompleteForButtons?.Invoke();
    }

    public void createCoreLayer()
    {
        layers.Add(Instantiate(uniqueLayers[thisLayer], Vector2.zero, Quaternion.identity, this.transform));
        onNewLayerCreated?.Invoke(layers[thisLayer].transform.localScale.x);
    }

    void createNewLayer(int prevLayer)
    {
        if (prevLayer == 0) { onTimerStart?.Invoke(); }
        if (prevLayer == 1) { onThingsStartGoingWrong?.Invoke(); }
        if (prevLayer == 9)
        {
            onVictoryRoyale?.Invoke();
            return;
        }
        AudioManager.Instance.PlaySFX("Level Solved");

        if (prevLayer == 0)
        {
            AudioManager.Instance.PlayMusic("2");
        }
        if (prevLayer == 2)
        {
            AudioManager.Instance.PlayMusic("3");
        }
        if (prevLayer == 3)
        {
            AudioManager.Instance.PlayMusic("4");
        }
        if (prevLayer == 4)
        {
            AudioManager.Instance.PlayMusic("5");
        }
        if (prevLayer == 8)
        {
            AudioManager.Instance.PlayMusic("6");
        }
        thisLayer = prevLayer + 1;
        layers.Add(Instantiate(uniqueLayers[thisLayer], Vector2.zero, Quaternion.identity, this.transform));
        layers[thisLayer].transform.localScale *= thisLayer * layerSizeIncrease;
        layerSizeIncrease *= layerSizeIncAccelerate;
        onNewLayerCreated?.Invoke(layers[thisLayer].transform.localScale.x);
    }
}
