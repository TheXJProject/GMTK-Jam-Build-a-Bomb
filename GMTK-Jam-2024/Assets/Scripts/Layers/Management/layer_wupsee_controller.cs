using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class layer_wupsee_controller : MonoBehaviour
{
    [SerializeField] float difficultyModifier = 0.6f; 

    [SerializeField] layer_controller layerController;
    [SerializeField] float startingCountdown = 10;

    [SerializeField] int startHiMultiTaskOdds = 10;
    [SerializeField] int startlowMultiTaskOdds = 5;

    System.Random rnd = new System.Random();
    bool wupseesBeHappening = false;
    int numLayers;

    bool repeat;
    int loopCounter;

    float countdownValue;
    float countdown;
    int hiMultiTaskOdds;
    int lowMultiTaskOdds;


    private void OnEnable()
    {
        layer_controller.onNewLayerCreated += IncreaseDifficulty;
        layer_controller.onThingsStartGoingWrong += beginWupsees;
        countdownValue = startingCountdown;
        countdown = countdownValue;

        hiMultiTaskOdds = startHiMultiTaskOdds;
        lowMultiTaskOdds = startlowMultiTaskOdds;
    }

    private void OnDisable()
    {
        layer_controller.onNewLayerCreated -= IncreaseDifficulty;
        layer_controller.onThingsStartGoingWrong -= beginWupsees;

    }

    private void Update()
    {
        numLayers = layerController.layers.Count;
        if (wupseesBeHappening)
        {
            if (countdown <= 0)
            {
                loopCounter = 0;
                do
                {
                    loopCounter++;
                    repeat = rnd.Next(hiMultiTaskOdds) < lowMultiTaskOdds;
                    if (layerController.layers[rnd.Next(numLayers - 1)].GetComponent<layer_task_controller>().MakeTaskGoWrong())
                    {
                        if (countdown <= 0) { countdown = countdownValue; }
                    }
                    else { countdown += 1; }
                    if (loopCounter > 100)
                    {
                        Debug.LogWarning("task gone wrong manager loop repeated too many times");
                        break;
                    }
                }
                while (repeat);
            }
            countdown -= Time.deltaTime;
        }
    }

    void IncreaseDifficulty(float obsolite)
    {
        if (wupseesBeHappening && numLayers >= 3)
        {
            hiMultiTaskOdds = (int)math.ceil((float)hiMultiTaskOdds * difficultyModifier);
            countdownValue *= difficultyModifier;
        }
    }

    void beginWupsees()
    {
        wupseesBeHappening = true;
    }
}
