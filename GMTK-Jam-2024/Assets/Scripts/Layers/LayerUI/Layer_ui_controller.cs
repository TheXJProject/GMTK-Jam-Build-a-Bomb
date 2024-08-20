using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer_ui_controller : MonoBehaviour
{
    [SerializeField] float initButtonHeight;
    [SerializeField] float gapHeight;
    [SerializeField] GameObject layerButton;
    
    [SerializeField] List<GameObject> layerButtons;

    int[] countTasksGoingWrong = new int[9];
    float topBottomHeight;
    float buttonHeight;
    float predictedTotalHeight;
    float currentSettingHeight;

    private void Start()
    {
        layerButtons.Add(Instantiate(layerButton, this.transform));
        layerButtons[0].GetComponent<button_for_layers>().representedLayer = 0;
        CalculateButtonSpacing();
        VisualiseAndFitCollider();
    }

    private void OnEnable()
    {
        layer_controller.onLayerSolved += AddNewLayerButton;
        task_activate_and_cancel.onTaskGoesWrong += AlertPlayerToLayer;
        task_setup_and_status.onWrongTaskCorrected += AttemptStopAlerting;
    }
    private void OnDisable()
    {
        layer_controller.onLayerSolved -= AddNewLayerButton;
        task_activate_and_cancel.onTaskGoesWrong -= AlertPlayerToLayer;
        task_setup_and_status.onWrongTaskCorrected -= AttemptStopAlerting;
    }

    void AlertPlayerToLayer(int layerNumber)
    {
        countTasksGoingWrong[layerNumber]++;
        layerButtons[layerNumber].GetComponent<button_for_layers>().LayerTaskWentWrong = true;
    }

    void AttemptStopAlerting(int layerNumber)
    {
        if (--countTasksGoingWrong[layerNumber] == 0)
        {
            layerButtons[layerNumber].GetComponent<button_for_layers>().LayerTaskWentWrong = false;
        }
    }

    void AddNewLayerButton(int layerNum)
    {
        layerButtons.Add(Instantiate(layerButton, this.transform));
        layerButtons[layerNum+1].GetComponent<button_for_layers>().representedLayer = layerNum+1;
        CalculateButtonSpacing();
        VisualiseAndFitCollider();
    }

    void CalculateButtonSpacing()
    {
        predictedTotalHeight = (layerButtons.Count * initButtonHeight) + ((layerButtons.Count - 1) * gapHeight);
        if (predictedTotalHeight > 1)
        {
            buttonHeight = (1f - ((layerButtons.Count - 1)) * gapHeight) / layerButtons.Count;
            topBottomHeight = 0;
        }
        else
        {
            buttonHeight = initButtonHeight;
            topBottomHeight = (1f - ((layerButtons.Count * buttonHeight) + (layerButtons.Count - 1) * gapHeight)) / 2f;
        }
    }

    void VisualiseAndFitCollider()
    {
        buttonHeight *= 0.68f;
        topBottomHeight *= 0.68f;

        currentSettingHeight = 0.25f + topBottomHeight;
        layerButtons[0].GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, currentSettingHeight);
        currentSettingHeight += buttonHeight;
        layerButtons[0].GetComponent<RectTransform>().anchorMax = new Vector2(0.95f, currentSettingHeight);

        for (int i = 1; i < layerButtons.Count; i++)
        {
            currentSettingHeight += gapHeight;
            layerButtons[i].GetComponent<RectTransform>().anchorMin = new Vector2(0.7f, currentSettingHeight);
            currentSettingHeight += buttonHeight;
            layerButtons[i].GetComponent<RectTransform>().anchorMax = new Vector2(0.95f, currentSettingHeight);
        }

        for (int i = 0; i < layerButtons.Count; i++)
        {
            layerButtons[i].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            layerButtons[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }

    }
}
