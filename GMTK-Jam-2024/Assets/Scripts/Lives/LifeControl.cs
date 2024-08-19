using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    [SerializeField] List<GameObject> lives = new List<GameObject>();

    private void OnEnable()
    {
        task_activate_and_cancel.onPlayerLetTaskGo += LooseLife;
    }

    private void OnDisable()
    {
        task_activate_and_cancel.onPlayerLetTaskGo -= LooseLife;
    }

    private void Start()
    {
        //if
    }

    private void LooseLife()
    {

    }

}
