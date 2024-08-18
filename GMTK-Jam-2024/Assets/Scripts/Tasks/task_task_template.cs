using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class task_task_template : MonoBehaviour
{
    /* DON'T CHANGE ANY OF THE FOLLOWING CODE */
    [SerializeField] task_setup_and_status task;
    [SerializeField] int setTaskDifficulty;

    Player_controller inputActions;
    InputAction leftClick;

    private void Awake()
    {
        inputActions = new Player_controller();
        task.taskDifficulty = setTaskDifficulty;
    }

    private void OnEnable()
    {
        leftClick = inputActions.mouse.leftClick;
        leftClick.Enable();

        /* 
            YOU CAN USE THE FOLLOWING, FUNCTIONS CAN ONLY BE ADDED THIS WAY IF THEY USE THE PARAMETER: InputAction.CallbackContext context:
           
            leftClick.performed += FunctionName; ----> this will perform the function "FunctionName" when you press left click
            leftClick.canceled += FunctionName;  ----> this will perform the function "FunctionName" when you release left click
           
            DO THIS ON THE LINE FOLLOWING THIS COMMENT, IF REQUIRED: */

    }

    /* DON'T CHANGE THE FOLLOWING CODE */
    private void OnDisable()
    {
        leftClick.Disable();
    }

    /* THE FOLLOWING WILL BE USEFUL:
       
        task.SetAmountTaskComplete(float amount)  ----> sets the percentage this task is complete for visualisation purposes ("amount" is a float between 0 and 1)
        task.TaskSolved();                        ----> run this line of code when the task has been completed (will automatically set "amount" the task is complete to 1)
        if (task.isBeingSolved && task.isFocused) ----> all code progressing the task should be written inside this if statement (the conditions for a task to be worked on) 
        {
            ...
        }
    */

    // Declare variables here:
    

    // You may or may not want to use Update(), there should be no need to use FixedUpdate() but ask if you disagree:
    private void Update()
    {

    }

    // Use the following function to reset the progress of the task (function is called when the player lets go of the keys prematurely)
    public void ResetTask()
    {
        
    }

    // Use the following space to create your own functions (REMEMBER: functions can only be assigned to mouse inputs using the "context" parameter as described above)
    
}
