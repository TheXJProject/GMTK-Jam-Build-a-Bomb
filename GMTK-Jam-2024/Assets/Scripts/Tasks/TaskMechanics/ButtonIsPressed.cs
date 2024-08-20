using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonIsPressed : MonoBehaviour
{
    public bool isPressed = false;
    public GameObject buttonPad;

    private void Update()
    {
        if(isPressed)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
    }

    public void PressButton()
    {
        if (buttonPad.GetComponent<task_buttons>().canPress)
        {
            isPressed = true;
        }
    }
}
