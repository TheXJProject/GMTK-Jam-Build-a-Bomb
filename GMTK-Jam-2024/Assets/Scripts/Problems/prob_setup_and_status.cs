using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class prob_setup_and_status : MonoBehaviour
{
    public static bool anyIsFocused; // whether or not the player is currently completing any problem

    public bool isSolved = false;
    public bool isBeingSolved = false;
    public bool isFocused = false; // Whether or not the player is currently completing this problem
    public float amountCompleted = 0f; // percentage way through the problem
    public int problemDifficulty = 1; // Difficulty is the number of keys needed to press to complete the problem
    public List<int> keysRequired = new List<int>(); // List of the keys needed in order to start this problem

    [SerializeField] control_keys_pressed control_Keys_Pressed;

    System.Random generator = new System.Random();
    int genNum = 0;
    int alphabetLength = 26;


    private void OnEnable()
    {
        SetKeysRequired();
        Debug.Log(control_Keys_Pressed.alphabet[keysRequired[0]]);
    }

    public void UnfocusProb()
    {
        anyIsFocused = false;
        isFocused = false;
    }
    public void FocusProb()
    {
        anyIsFocused = true;
        isFocused = true;
    }
    void ProblemSolved()
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
        for (int i = 0; i < problemDifficulty; i++)
        {
            if (control_Keys_Pressed.keysPressed[keysRequired[i]] == 0 )
            {
                return false;
            }
        }
        return true;
    }
}
