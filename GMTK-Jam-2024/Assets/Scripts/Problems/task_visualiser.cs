using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class task_visualiser : MonoBehaviour
{
    [SerializeField] task_setup_and_status task;
    [SerializeField] SpriteRenderer sprite;

    private void Update()
    {
        if ( task.isSolved ) { DisplaySolved(); }
        else if (task.isBeingSolved) { DisplayBeingSolved(); }
        else { DisplayNotSolved(); }
    }

    public void DisplayNotSolved()
    {
        if (task.isFocused) { sprite.color = new Color(1, 0, 0); }
        else { sprite.color = new Color(0.5f, 0, 0); }
    }

    public void DisplayBeingSolved()
    {
        sprite.color = new Color(1, 0.5f, 0); 
    }

    public void DisplaySolved()
    {
        if (task.isFocused) { sprite.color = new Color(0, 1, 0); }
        else { sprite.color = new Color(0, 0.5f, 0); }
    }
}
