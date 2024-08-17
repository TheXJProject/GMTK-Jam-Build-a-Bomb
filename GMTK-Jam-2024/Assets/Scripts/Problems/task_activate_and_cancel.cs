using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class task_activate_and_cancel : MonoBehaviour
{
    public control_keys_pressed keyController;
    public task_setup_and_status task;

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
        if (!task_setup_and_status.anyIsFocused && !task.isSolved)
        {
            task.FocusTask();
        }
    }

    private void Update()
    {
        if (task.isFocused && !task.isBeingSolved)
        {
            numReqKeysPressed = 0;
            for (global::System.Int32 i = 0; i < task.taskDifficulty; i++)
            {
                if (!(keyController.keysPressed[task.keysRequired[i]] == 1)) { break; }
                numReqKeysPressed++;
            }
            if (numReqKeysPressed == task.taskDifficulty)
            {
                task.isBeingSolved = true;
            }
        }
        else if (task.isBeingSolved)
        {
            numReqKeysPressed = 0;
            for (global::System.Int32 i = 0; i < task.taskDifficulty; i++)
            {
                if (!(keyController.keysPressed[task.keysRequired[i]] == 1)) { break; }
                numReqKeysPressed++;
            }
            if (numReqKeysPressed != task.taskDifficulty)
            {
                task.isBeingSolved = false;
            }
        }
    }

    void AttemptToUnfocus(InputAction.CallbackContext context)
    {
        if (task.isFocused)
        {
            task.UnfocusTask();
        }
    }
}
