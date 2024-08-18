using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class task_setup_and_status : MonoBehaviour
{
    public static bool anyIsFocused; // whether or not the player is currently completing any task

    public bool isSolved = false;
    public bool isBeingSolved = false;
    public bool isFocused = false; // Whether or not the player is currently completing this task
    public float amountCompleted = 0f; // Percentage way through the task
    public int taskDifficulty; // Difficulty is the number of keys needed to press to complete the task
    public List<int> keysRequired = new List<int>(); // List of the keys needed in order to start this task

    control_keys_pressed keyController;

    System.Random generator = new System.Random();
    int genNum = 0;
    int alphabetLength = 26;


    private void OnEnable()
    {
        keyController = GameObject.Find("/Game Controllers/Controller for Keys Pressed").GetComponent<control_keys_pressed>();
        SetKeysRequired();

        // Debug log the keys required
        for (int i = 0; i < taskDifficulty; i++)
        {
            Debug.Log(keyController.alphabet[keysRequired[i]]);
        }
    }

    public void UnfocusTask()
    {
        anyIsFocused = false;
        isFocused = false;
    }
    public void FocusTask()
    {
        anyIsFocused = true;
        isFocused = true;
    }

    public void SetAmountTaskComplete(float amount)
    {
        amountCompleted = amount;
    }

    public void TaskSolved()
    {
        isSolved = true;
        isBeingSolved = false;
        isFocused = false;
        anyIsFocused = false;
        amountCompleted = 1f;
    } 

    void SetKeysRequired()
    {
        for (int i = 0; i < taskDifficulty; i++)
        {
            genNum = generator.Next(0, alphabetLength);
            if (!keysRequired.Contains(genNum))
                keysRequired.Add(genNum);    
        }
    }

    bool KeysArePressed()
    {
        for (int i = 0; i < taskDifficulty; i++)
        {
            if (keyController.keysPressed[keysRequired[i]] == 0 )
            {
                return false;
            }
        }
        return true;
    }
}
