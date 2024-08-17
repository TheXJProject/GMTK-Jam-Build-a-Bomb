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
    public int taskDifficulty = 1; // Difficulty is the number of keys needed to press to complete the taskk
    public List<int> keysRequired = new List<int>(); // List of the keys needed in order to start this task

    [SerializeField] control_keys_pressed keyController;

    System.Random generator = new System.Random();
    int genNum = 0;
    int alphabetLength = 26;


    private void OnEnable()
    {
        SetKeysRequired();
        Debug.Log(keyController.alphabet[keysRequired[0]]);
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
    void TaskSolved()
    {
        isSolved = true;
        amountCompleted = 1f;
    } 

    void SetKeysRequired()
    {
        genNum = generator.Next(0, alphabetLength);
        if (!keysRequired.Contains(genNum))
            keysRequired.Add(genNum);
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
