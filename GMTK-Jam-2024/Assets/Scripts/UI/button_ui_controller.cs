using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class button_ui_controller : MonoBehaviour
{
    [SerializeField] float initButtonHeight;
    [SerializeField] float gapHeight;
    [SerializeField] GameObject keyButton;

    [SerializeField] List<GameObject> keyButtons = new List<GameObject>();
    [SerializeField] List<int> keyButtonKeys = new List<int>();

    control_keys_pressed keyController;

    int count;
    float buttonHeight;
    float predictedTotalHeight;
    float currentSettingHeight;

    private void OnEnable()
    {
        keyController = GameObject.Find("/Game Managers/Manager for Keys Pressed").GetComponent<control_keys_pressed>();
        task_setup_and_status.onTaskFocusForButtons += AddAllButtons;
        task_setup_and_status.onTaskUnFocus += RemoveUndeededButtons;
        control_keys_pressed.onKeyLetGo += RemoveUndeededButtons;
        layer_controller.onTaskCompleteForButtons += RemoveUndeededButtons;
    }

    private void OnDisable()
    {
        task_setup_and_status.onTaskFocusForButtons -= AddAllButtons;
        task_setup_and_status.onTaskUnFocus -= RemoveUndeededButtons;
        control_keys_pressed.onKeyLetGo -= RemoveUndeededButtons;
        layer_controller.onTaskCompleteForButtons -= RemoveUndeededButtons;
    }

    void AddAllButtons(List<int> newLetters)
    {
        for (int i = 0; i < newLetters.Count; i++)
        {
            AddNewKeyButton(newLetters[i]);
        }
    }

    void AddNewKeyButton(int letter)
    {
        RemoveUndeededButtons();
        keyButtons.Add(Instantiate(keyButton, this.transform));
        keyButtonKeys.Add(letter);
        keyButtons[keyButtons.Count - 1].GetComponent<key_buttons>().letter = letter;
        CalculateButtonSpacing();
        VisualiseButtons();
    }


    void CalculateButtonSpacing()
    {
        predictedTotalHeight = (keyButtons.Count * initButtonHeight) + ((keyButtons.Count - 1) * gapHeight);
        if (predictedTotalHeight > 0.62)
        {
            buttonHeight = (0.62f - ((keyButtons.Count - 1)) * gapHeight) / keyButtons.Count;
        }
        else { buttonHeight = initButtonHeight; }
    }

    void VisualiseButtons()
    {
        if (keyButtons.Count == 0) { return; }
        currentSettingHeight = 0.845f;
        keyButtons[0].GetComponent<RectTransform>().anchorMax = new Vector2(0.06f, currentSettingHeight);
        currentSettingHeight -= buttonHeight;
        keyButtons[0].GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, currentSettingHeight);

        for (int i = 1; i < keyButtons.Count; i++)
        {
            currentSettingHeight -= gapHeight;
            keyButtons[i].GetComponent<RectTransform>().anchorMax = new Vector2(0.06f, currentSettingHeight);
            currentSettingHeight -= buttonHeight;
            keyButtons[i].GetComponent<RectTransform>().anchorMin = new Vector2(0.02f, currentSettingHeight);
        }

        for (int i = 0; i < keyButtons.Count; i++)
        {
            keyButtons[i].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            keyButtons[i].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        }
    }

    void RemoveUndeededButtons()
    {
        if (keyButtons.Count == 0) { return; }
        count = 0;
        int i = 0;
        while (true)
        {
            Debug.Log(i);
            count++;
            if (i >= keyButtons.Count) { break; }
            if (keyController.keysPressed[keyButtonKeys[i]] == 0)
            {
                Destroy(keyButtons[i]);
                keyButtons.RemoveAt(i);
                keyButtonKeys.RemoveAt(i);
            }
            else { i++; }
            if (count > 10000)
            {
                Debug.LogWarning("Removing all undeeded buttons took too long");
                break;
            }
        }
    }
}
