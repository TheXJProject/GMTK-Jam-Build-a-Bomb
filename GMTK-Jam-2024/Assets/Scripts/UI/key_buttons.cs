using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class key_buttons : MonoBehaviour
{
    public string[] alphabet = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    public int letter;

    control_keys_pressed keyController;
    private void OnEnable()
    {
        keyController = GameObject.Find("/Game Managers/Manager for Keys Pressed").GetComponent<control_keys_pressed>();
    }

    private void Update()
    {
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = alphabet[letter];
        if (keyController.keysPressed[letter] == 1) { gameObject.GetComponent<Image>().color = Color.yellow;  }
        else { gameObject.GetComponent<Image>().color = Color.white; }
    }
}
