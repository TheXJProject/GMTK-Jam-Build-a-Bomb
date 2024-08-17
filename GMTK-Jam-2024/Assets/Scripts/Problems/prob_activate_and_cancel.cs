using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class prob_activate_and_cancel : MonoBehaviour
{
    public control_keys_pressed keyController;
    public prob_setup_and_status prob;

    Player_controller inputActions;
    InputAction rightClick;
    int numReqKeysPressed;

    private void Awake()
    {
        inputActions = new Player_controller();
    }

    private void OnEnable()
    {
        rightClick = inputActions.mouse.rightClick;
        rightClick.Enable();
        rightClick.performed += AttemptToUnfocus;
    }

    private void OnDisable()
    {
        rightClick.Disable();
    }
    private void OnMouseDown()
    {
        if (!prob_setup_and_status.anyIsFocused && !prob.isSolved)
        {
            prob.FocusProb();
        }
    }

    private void Update()
    {
        if (prob.isFocused && !prob.isBeingSolved)
        {
            numReqKeysPressed = 0;
            for (global::System.Int32 i = 0; i < prob.problemDifficulty; i++)
            {
                if (!(keyController.keysPressed[prob.keysRequired[i]] == 1)) { break; }
                numReqKeysPressed++;
            }
            if (numReqKeysPressed == prob.problemDifficulty)
            {
                prob.isBeingSolved = true;
            }
        }
        else if (prob.isBeingSolved)
        {
            numReqKeysPressed = 0;
            for (global::System.Int32 i = 0; i < prob.problemDifficulty; i++)
            {
                if (!(keyController.keysPressed[prob.keysRequired[i]] == 1)) { break; }
                numReqKeysPressed++;
            }
            if (numReqKeysPressed != prob.problemDifficulty)
            {
                prob.isBeingSolved = false;
            }
        }
    }

    void AttemptToUnfocus(InputAction.CallbackContext context)
    {
        if (prob.isFocused)
        {
            prob.UnfocusProb();
        }
    }
}
