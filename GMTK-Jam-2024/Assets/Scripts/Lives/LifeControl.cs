using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeControl : MonoBehaviour
{
    [SerializeField] List<GameObject> lives = new List<GameObject>();

    [Range(0.01f, 1f)]
    [SerializeField] float iFrameDuration = 0.5f;

    private bool iFrames = false;

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
        PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.noWin;

        if (PlayerTracking.Tracker.hardMode)
        {
            lives[1].GetComponent<OnOff>().UseLife();
            lives[2].GetComponent<OnOff>().UseLife();
        }
    }

    private void LooseLife()
    {
        if (!iFrames)
        {
            for (int i = lives.Count - 1; i >= 0; i--)
            {
                if (!lives[i].GetComponent<OnOff>().lifeUsed)
                {
                    StartCoroutine(IFrameTimer());
                    lives[i].GetComponent<OnOff>().UseLife();
                    break;
                }
            }

            if (lives[0].GetComponent<OnOff>().lifeUsed)
            {
                PlayerTracking.Tracker.currentWinType = PlayerTracking.winType.Loss;

                Debug.Log("Player looses bigtime :'(");

            }
        }
    }

    IEnumerator IFrameTimer()
    {
        float elapsedTime = 0f;
        float interval = 0.01f;
        iFrames = true;

        do
        {
            yield return new WaitForSeconds(interval);

            elapsedTime += interval;

            if (elapsedTime >= iFrameDuration)
            {
                iFrames = false;
            }

        } while (elapsedTime < iFrameDuration);
    }
}
