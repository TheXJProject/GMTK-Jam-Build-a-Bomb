using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer_ui_controller : MonoBehaviour
{
    [SerializeField] float initButtonHeight;
    [SerializeField] float gapHeight;
    [SerializeField] GameObject layerButton;
    
    [SerializeField] List<GameObject> layerButtons;
    
    float topBottomHeight;
    float buttonHeight;
    float predictedTotalHeight;
    float currentSettingHeight;

    private void Start()
    {
        layerButtons.Add(Instantiate(layerButton, this.transform));
        CalculateButtonSpacing();
        VisualiseButtons();
    }

    private void OnEnable()
    {
        layer_task_controller.onLayerSolved += AddNewLayerButton;
    }
    private void OnDisable()
    {
        layer_task_controller.onLayerSolved -= AddNewLayerButton;
    }

    void AddNewLayerButton(int layerNum)
    {
        layerButtons.Add(Instantiate(layerButton, this.transform));
        CalculateButtonSpacing();
        VisualiseButtons();
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

    void VisualiseButtons()
    {
        currentSettingHeight = topBottomHeight;
        layerButtons[0].GetComponent<RectTransform>().anchorMin = new Vector2(0, currentSettingHeight);
        currentSettingHeight += buttonHeight;
        layerButtons[0].GetComponent<RectTransform>().anchorMax = new Vector2(1, currentSettingHeight);

        for (int i = 1; i < layerButtons.Count; i++)
        {
            currentSettingHeight += gapHeight;
            layerButtons[i].GetComponent<RectTransform>().anchorMin = new Vector2(0, currentSettingHeight);
            currentSettingHeight += buttonHeight;
            layerButtons[i].GetComponent<RectTransform>().anchorMax = new Vector2(1, currentSettingHeight);
        }

        for (int i = 0; i < layerButtons.Count; i++)
        {
            layerButtons[i].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            layerButtons[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }

    }
}
