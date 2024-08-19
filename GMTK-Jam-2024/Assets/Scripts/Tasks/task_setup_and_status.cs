using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Linq;

public class task_setup_and_status : MonoBehaviour
{
    public static event Action<int> onTaskComplete;
    public static event Action<int> onWrongTaskCorrected;
    public static bool anyIsFocused; // whether or not the player is currently completing any task

    public bool isSolved = false;
    public bool isBeingSolved = false;
    public bool hasGoneWrong = false;
    public bool isFocused = false; // Whether or not the player is currently completing this task
    public float amountCompleted = 0f; // Percentage way through the task
    public int taskDifficulty; // Difficulty is the number of keys needed to press to complete the task
    public int taskLayer; // Set by the layer that spawns it
    public List<int> keysRequired = new List<int>(); // List of the keys needed in order to start this task

    control_keys_pressed keyController;

    System.Random generator = new System.Random();
    int genNum = 0;
    int alphabetLength = 26;


    private void OnEnable()
    {
        keyController = GameObject.Find("/Game Managers/Manager for Keys Pressed").GetComponent<control_keys_pressed>();
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

        if (hasGoneWrong) { onWrongTaskCorrected?.Invoke(taskLayer); }
        onTaskComplete?.Invoke(taskLayer);
        
    } 

    void SetKeysRequired() // Sets unique keys for a task, if the task has gone wrong then the keys won't be ones that are being pressed
    {
        int j;
        keysRequired.Clear();

        for (int i = 0; i < taskDifficulty; i++)
        {
            genNum = generator.Next(0, alphabetLength);
            if (!keysRequired.Contains(genNum)) { keysRequired.Add(genNum); }
        }

        if (hasGoneWrong)
        { 
            for (j = 0; j < taskDifficulty; j++)
            {
                if (keyController.keysPressed.Contains(keysRequired[j])) { break; }
            }
            if (keyController.keysPressed.Contains(keysRequired[j])) { SetKeysRequired(); }
        }
    }

    bool KeysArePressed() // Checks the keys for this task are all being pressed down
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
