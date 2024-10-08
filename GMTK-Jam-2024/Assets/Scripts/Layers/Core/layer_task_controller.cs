using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using System.Linq;

public class layer_task_controller : MonoBehaviour
{
    public List<GameObject> tasksSpawned;
    [SerializeField] int minNumberTasks = 3;
    [SerializeField] int maxNumberTasks = 6;

    control_keys_pressed keyController;
    [SerializeField] List<GameObject> uniqueTasks;
    [SerializeField] List<Vector2> taskPositions;

    public int layer;
    public int topLayerTasksSolved = 0;

    [SerializeField] float radiusForTaskCollider;
    [SerializeField] float minRadiusForNoSpawns;
    [SerializeField] float maxRadiusForNoSpawns;

    System.Random rnd = new System.Random();
    int temp;
    int numberTasksSpawned;
    Vector2 tempNewLoc1;
    Vector2 tempNewLoc2;

    private void OnEnable()
    {
        keyController = GameObject.Find("/Game Managers/Manager for Keys Pressed").GetComponent<control_keys_pressed>();
        numberTasksSpawned = rnd.Next(minNumberTasks, (maxNumberTasks + 1));
        SpawnTasks();
    }

    Vector2 GetNextTaskSpawnLoc() // Gets location for spawn within a circle such that it is between the minimum and maxium radius
    {
        tempNewLoc1 = UnityEngine.Random.insideUnitCircle * (maxRadiusForNoSpawns - radiusForTaskCollider);
        int loopCount = 0;
        while (Vector2.Distance(Vector2.zero, tempNewLoc1) < (minRadiusForNoSpawns + radiusForTaskCollider)) 
        {
            loopCount++;
            tempNewLoc1 = UnityEngine.Random.insideUnitCircle * (maxRadiusForNoSpawns - radiusForTaskCollider);
            if (loopCount > 10000)
            {
                Debug.LogWarning("Checking for clashes with the minimum and maximum radius took too long");
                break;
            }
        }
        return tempNewLoc1;
    }

    bool ClashesWithOtherTasks(Vector2 taskPos)
    {
        for (int i = 0; i < taskPositions.Count; i++)
        {
            if (Vector2.Distance(taskPositions[i], taskPos) < (2 * radiusForTaskCollider)) { return true; }
        }
        return false;
    }

    public void SpawnTasks()
    {
        tempNewLoc2 = GetNextTaskSpawnLoc();
        for (int i = 0; i < numberTasksSpawned; i++)
        {
            int loopCount = 0;
            while (ClashesWithOtherTasks(tempNewLoc2))
            {
                loopCount++;
                tempNewLoc2 = GetNextTaskSpawnLoc();
                if (loopCount > 100000)
                {
                    Debug.LogWarning("Checking for clashes with other tasks took too long");
                    break;
                }
            }
            taskPositions.Add(tempNewLoc2);
        }
        for (int i = 0; i < numberTasksSpawned; i++)
        {
            temp = rnd.Next(uniqueTasks.Count);
            uniqueTasks[temp].GetComponent<task_setup_and_status>().taskLayer = layer;
            tasksSpawned.Add(Instantiate(uniqueTasks[temp], new Vector2(this.transform.position.x, this.transform.position.y) + taskPositions[i], Quaternion.identity, this.transform));
        }
    }

    public bool CheckFinishedThisLayer()
    {
        for (int i = 0; i < numberTasksSpawned; i++)
        {
            if (!tasksSpawned[i].GetComponent<task_setup_and_status>().isSolved) { return false; }
        }
        return true;
    }

    public bool MakeTaskGoWrong()
    {
        if (keyController.keysPressed.Sum() >= 5) 
        {
            return false; 
        }
        return tasksSpawned[rnd.Next(numberTasksSpawned)].GetComponent<task_activate_and_cancel>().TaskGoesWrong();
    }
}
