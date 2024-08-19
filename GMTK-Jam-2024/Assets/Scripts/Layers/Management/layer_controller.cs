using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class layer_controller : MonoBehaviour
{
    public static event Action onTimerStart;
    public static event Action<int> onLayerSolved;
    public static event Action<float> onNewLayerCreated;

    [SerializeField] public static int tasksGoingWrong = 0;

    [SerializeField] float layerSizeIncrease = 3f;
    [SerializeField] float layerSizeIncAccelerate = 2f;
    [SerializeField] List<GameObject> uniqueLayers;
    [SerializeField] List<GameObject> layers;

    int thisLayer = 0;
    private void OnEnable()
    {
        onLayerSolved += createNewLayer;
        task_setup_and_status.onTaskComplete += CheckFinishedLayer;
        camera_controller.onCameraReady += createCoreLayer;
        task_activate_and_cancel.onTaskGoesWrong += TasksGoneWrongInc;
        task_setup_and_status.onWrongTaskCorrected += TasksGoneWrongDec;
    }

    private void OnDisable()
    {
        onLayerSolved -= createNewLayer;
        task_setup_and_status.onTaskComplete -= CheckFinishedLayer;
        camera_controller.onCameraReady -= createCoreLayer;
        task_activate_and_cancel.onTaskGoesWrong -= TasksGoneWrongInc;
        task_setup_and_status.onWrongTaskCorrected -= TasksGoneWrongDec;
    }

    void CheckFinishedLayer(int taskLayer)
    {
        for (int i = 0; i < layers.Count; i++)
        {
            if (!layers[i].GetComponent<layer_task_controller>().TestLayerIsComplete()) { return; }
        }
        onLayerSolved?.Invoke(thisLayer);
    }

    void TasksGoneWrongInc(int obsolite)
    {
        tasksGoingWrong++;
    }

    void TasksGoneWrongDec()
    {
        tasksGoingWrong--;
    }

    public void createCoreLayer()
    {
        layers.Add(Instantiate(uniqueLayers[thisLayer], Vector2.zero, Quaternion.identity, this.transform));
        onNewLayerCreated?.Invoke(layers[thisLayer].transform.localScale.x);
    }

    void createNewLayer(int prevLayer)
    {
        if (prevLayer == 0) { onTimerStart?.Invoke(); }
        thisLayer = prevLayer + 1;
        layers.Add(Instantiate(uniqueLayers[thisLayer], Vector2.zero, Quaternion.identity, this.transform));
        layers[thisLayer].transform.localScale *= thisLayer * layerSizeIncrease;
        layerSizeIncrease *= layerSizeIncAccelerate;
        onNewLayerCreated?.Invoke(layers[thisLayer].transform.localScale.x);
    }
}
