using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class task_activate_and_cancel : MonoBehaviour
{
    public static event Action onPlayerLetTaskGo;
    public static event Action<int> onTaskGoesWrong;

    control_keys_pressed keyController;
    [SerializeField] task_setup_and_status task;
    
    Player_controller inputActions;
    InputAction rightClick;
    int numReqKeysPressed;

    private void Awake()
    {
        inputActions = new Player_controller();
    }

    private void OnEnable()
    {
        keyController = GameObject.Find("/Game Managers/Manager for Keys Pressed").GetComponent<control_keys_pressed>();
        rightClick = inputActions.mouse.rightClick;
        rightClick.Enable();
        rightClick.performed += AttemptToUnfocus;

        gameObject.transform.localScale *= (1f - ((float)task.taskLayer / 20f));
    }

    private void OnDisable()
    {
        rightClick.Disable();
    }

    private void OnMouseDown()
    {
        if (!task_setup_and_status.anyIsFocused && !task.isSolved && (camera_controller.currentLevelFocused == task.taskLayer)) // Task can only be focused if no other task is focused and it isn't already solved
        {
            task.FocusTask();
        }
    }

    private void Update()
    {
        if (task.isFocused && !task.isBeingSolved) // This if statement section chceks if all the correct keys are being pressed while the task is focused
        {
            numReqKeysPressed = 0;
            for (global::System.Int32 i = 0; i < task.taskDifficulty; i++)
            {
                if (!(keyController.keysPressed[task.keysRequired[i]] == 1)) { break; }
                numReqKeysPressed++;
            }
            if (numReqKeysPressed == task.taskDifficulty)
            {
                BeginSolving();
            }
        }
        else if (task.isBeingSolved) // This if statement section checks tasks that are being solved to see if the player let go of the correct keys
        {
            numReqKeysPressed = 0;
            for (global::System.Int32 i = 0; i < task.taskDifficulty; i++)
            {
                if (!(keyController.keysPressed[task.keysRequired[i]] == 1)) { break; }
                numReqKeysPressed++;
            }
            if (numReqKeysPressed != task.taskDifficulty)
            {
                LetGoWhileSolving();
            }
        }
    }

    void BeginSolving()
    {
        task.isBeingSolved = true;
    }

    void LetGoWhileSolving()
    {
        AudioManager.Instance.PlaySFX("Task Fail");
        task.isBeingSolved = false;
        onPlayerLetTaskGo?.Invoke();
    }

    void AttemptToUnfocus(InputAction.CallbackContext context)
    {
        if (task.isFocused)
        {
            task.UnfocusTask();
        }
    }

    public bool TaskGoesWrong()
    {
        AudioManager.Instance.PlaySFX("Task Goes Wrong");
        if (task.hasGoneWrong) { return false; }
        if (task.taskDifficulty - keyController.keysPressed.Sum() <= 0) { return false; }
        task.isSolved = false;
        task.isBeingSolved = false;
        task.isFocused = false;
        task_setup_and_status.amountCompleted = 0f;
        task.hasGoneWrong = true;

        onTaskGoesWrong?.Invoke(task.taskLayer);
        return true;
    }
}
