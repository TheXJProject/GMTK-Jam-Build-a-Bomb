using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOff : MonoBehaviour
{
    public bool lifeUsed = false;

    [Range(0.01f,1f)]
    [SerializeField] float flickerSpeed = 0.1f;
    [SerializeField] float flickerDuration = 0.5f;

    [SerializeField] GameObject LifeOn;
    [SerializeField] GameObject LifeOff;
    
    public void UseLife()
    {
        becomeOff();
        StartCoroutine(Flicker());
        becomeOff();
    }

    public void becomeOff()
    {
        lifeUsed = true;
        LifeOn.SetActive(false);
        LifeOff.SetActive(true);
    }

    public void becomeOn()
    {
        lifeUsed = false;
        LifeOn.SetActive(true);
        LifeOff.SetActive(false);
    }

    IEnumerator Flicker()
    {
        float elapsedTime = 0f;

        do
        {
            if (lifeUsed)
            {
                becomeOn();
            }
            else
            {
                becomeOff();
            }

            yield return new WaitForSeconds(flickerSpeed);

            elapsedTime += flickerSpeed;

        } while (elapsedTime < flickerDuration);
    }
}
