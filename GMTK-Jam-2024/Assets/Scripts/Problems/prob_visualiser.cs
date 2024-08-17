using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prob_visualiser : MonoBehaviour
{
    [SerializeField] prob_setup_and_status prob;
    [SerializeField] SpriteRenderer sprite;

    private void Update()
    {
        if ( prob.isSolved ) { DisplaySolved(); }
        else if (prob.isBeingSolved) { DisplayBeingSolved(); }
        else { DisplayNotSolved(); }
    }

    public void DisplayNotSolved()
    {
        if (prob.isFocused) { sprite.color = new Color(1, 0, 0); }
        else { sprite.color = new Color(0.5f, 0, 0); }
    }

    public void DisplayBeingSolved()
    {
        sprite.color = new Color(1, 0.5f, 0); 
    }

    public void DisplaySolved()
    {
        if (prob.isFocused) { sprite.color = new Color(0, 1, 0); }
        else { sprite.color = new Color(0, 0.5f, 0); }
    }
}
